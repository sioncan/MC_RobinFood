using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using GooglePlayGames.BasicApi;
using GooglePlayGames;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerIstance;
    public static bool isPaused;

    // account google
    public Button signIn;
    public Text signInMessage;

    // resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // references
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public GameObject hud;
    public GameObject menu;
    public Animator deathMenuAnim;
    public Animator joystickAnim;
    public Animator attButtonAnim;
    public GameObject attackButton;
    public GameObject music;

    // logic
    public int coins;
    public int experience;

    // google account
    private void Start()
    {   // setup iniziale google account
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .RequestIdToken()
            .RequestServerAuthCode(false)
            .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        signIn.onClick.RemoveAllListeners();
        signIn.onClick.AddListener(SignInGooglePlayGames);

        SignInGooglePlayGames();
    }

    // google account
    private void SignInGooglePlayGames()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) =>
        {
            signInMessage.text = result.ToString();
            signIn.onClick.AddListener(SignoutGooglePlay);
        });
    }

    // google account
    private void SignoutGooglePlay()
    {
        PlayGamesPlatform.Instance.SignOut();
        signInMessage.text = "Sign Out";
        SignInGooglePlayGames();
    }

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    private void Awake()
    {   // dobbiamo avere un'unica istanza del GameManager (il primo GameManager che trova nella Scena)
        if(GameManager.gameManagerIstance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            Destroy(attackButton);
            Destroy(music);
            return;
        }
        gameManagerIstance = this;

        // ogni volta che carico una scena, il game manager esegue tutte le funzioni che ho aggiunto
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // potenzia l'arma
    public bool TryUpgradeWeapon()
    {
        // controllo se abbiamo l'arma finale (livello max)
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if(coins >= weaponPrices[weapon.weaponLevel])
        {
            coins -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    // xp/levelling system
    public int GetCurrentLevel()
    {
        int result = 0;
        int add = 0;

        while(experience >= add)
        {
            add += xpTable[result];
            result++;
            if (result == xpTable.Count) // controllo se sono max level
                return result;
        }
        return result;
    }

    public int GetXpToLevel(int level)
    {
        int result = 0;
        int xp = 0;

        while(result < level)
        {
            xp += xpTable[result];
            result++;
        }
        return xp;
    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel()) // se siamo saliti di livello
            OnLevelUp();
    }

    public void OnLevelUp()
    {
        player.OnLevelUp();
        OnHitpointChange();
    }

    // Health/hipoint bar
    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(ratio, 1, 1);
    }

    // salva lo stato del gioco
    /*
     * 0 int playerSkin
     * 1 int money
     * 2 int experience
     * 3 int weaponLevel
     */
    public void SaveState()
    {
        string saving = "";
        saving += "0" + "|";
        saving += coins.ToString() + "|";
        saving += experience.ToString() + "|";
        saving += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", saving);
    }

    // carica il gioco salvato
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;
        // se non ho salvato, non carico il salvaggio (che non esiste)
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        coins = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        if(GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());
        weapon.SetWeaponLevel(int.Parse(data[3]));
    }    

    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    public void PauseGame()
    {
        isPaused = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
    }

    // azzera tutti i dati
    public void ResetGame()
    {
        SceneManager.LoadScene("Main");
        deathMenuAnim.SetTrigger("Hide");
        player.SetLevel(1);
        player.Respawn();
        this.coins = 0;
        this.experience = 0;
        player.SwapSprite(3);
        player.GetComponentInChildren<Weapon>().SetWeaponLevel(0);
    }
}
