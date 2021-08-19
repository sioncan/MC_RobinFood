using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{
    // texts
    public Text levelText, hitpointText, coinsText, upgradeCostText, xpText;
    // logic
    private int currentSkinSelection = 0;
    public Image skinSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    // selezione skin del player
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentSkinSelection++;
            // se andiamo oltre l'ultima skin, ricomincia dalla prima
            if (currentSkinSelection == GameManager.gameManagerIstance.playerSprites.Count)
                currentSkinSelection = 0;

            OnSelectedChange();
        } else
        {
            currentSkinSelection--;
            // se andiamo prima della prima skin, ricomincia dall'ultima
            if (currentSkinSelection < 0)
                currentSkinSelection = GameManager.gameManagerIstance.playerSprites.Count - 1;

            OnSelectedChange();
        }
    }

    // cambio skin del player
    private void OnSelectedChange()
    {
        skinSelectionSprite.sprite = GameManager.gameManagerIstance.playerSprites[currentSkinSelection];
    }

    // upgrade arma, se l'upgrade è avvenuto con successo, allora aggiorno l'immagine nel menu
    public void OnUpgradeClick()
    {
        if (GameManager.gameManagerIstance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    // aggiorna le info del player
    public void UpdateMenu()
    {
        weaponSprite.sprite = GameManager.gameManagerIstance.weaponSprites[GameManager.gameManagerIstance.weapon.weaponLevel];
        upgradeCostText.text = "";
        levelText.text = "";
        hitpointText.text = GameManager.gameManagerIstance.player.hitpoint.ToString();
        coinsText.text = GameManager.gameManagerIstance.coins.ToString();
        xpText.text = "";
        xpBar.localScale = new Vector3(0.5f, 0, 0);
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
