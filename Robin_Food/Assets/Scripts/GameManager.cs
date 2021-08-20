using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerIstance;

    // resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // references
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;

    // logic
    public int coins;
    public int experience;

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
            return;
        }
        gameManagerIstance = this;

        // ogni volta che carico una scena, il game manager esegue tutte le funzioni che ho aggiunto
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
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
        // se non ho salvato, non carico il salvaggio (che non esiste)
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        coins = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        if(GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());
        weapon.SetWeaponLevel(int.Parse(data[3]));

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
        

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
