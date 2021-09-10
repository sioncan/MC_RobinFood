using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    // sprite chest vuota da sostuituire una volta perta
    public Sprite EmptyChest;
    // qanti coins sono nella chest
    public int coinsAmount;

    protected override void OnCollect()
    {
        if (!collected)
        {
            // se la chest non è gia stata aperta, la flaggo come aperta, cambio lo sprite in aperto, 
            base.OnCollect();
            GetComponent<SpriteRenderer>().sprite = EmptyChest;
            GameManager.gameManagerIstance.coins += coinsAmount;
            GameManager.gameManagerIstance.ShowText("+" + coinsAmount + " coins!", 30, Color.yellow, transform.position, Vector3.up * 25, 2f);
            GetComponent<AudioSource>().Play();            
        }
    }
}
