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
        GameManager.gameManagerIstance.player.SwapSprite(currentSkinSelection);
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
        // upgrade weapon cost
        if(GameManager.gameManagerIstance.weapon.weaponLevel == GameManager.gameManagerIstance.weaponPrices.Count)
            upgradeCostText.text = "Max";
        else
            upgradeCostText.text = GameManager.gameManagerIstance.weaponPrices[GameManager.gameManagerIstance.weapon.weaponLevel].ToString() + "C";
        
        levelText.text = GameManager.gameManagerIstance.GetCurrentLevel().ToString();
        hitpointText.text = GameManager.gameManagerIstance.player.hitpoint.ToString();
        coinsText.text = GameManager.gameManagerIstance.coins.ToString();
        // xp bar
        int currentLevel = GameManager.gameManagerIstance.GetCurrentLevel();
        if (currentLevel == GameManager.gameManagerIstance.xpTable.Count) // se sono max level
        {
            xpText.text = GameManager.gameManagerIstance.experience.ToString() + " total xp points"; // mostra xp tot
            xpBar.localScale = Vector3.one; // riempie tutta la barra dell'xp
        }
        else
        {
            int prevLevelXp = GameManager.gameManagerIstance.GetXpToLevel(currentLevel - 1);
            int currLevelXp = GameManager.gameManagerIstance.GetXpToLevel(currentLevel);
            int diff = currLevelXp - prevLevelXp; // xp necessaria per salire di livello
            int currXpIntoLevel = GameManager.gameManagerIstance.experience - prevLevelXp; // xp fatta nel livello attuale
            float completionRatio = (float)currXpIntoLevel / (float)diff; // % di xp fatta nel livello attuale->prossimo
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + " / " + diff + " exp.";
        }
    }
}
