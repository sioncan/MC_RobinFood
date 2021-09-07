using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : Collidable
{
    private int healingAmount = 5;
    private float healCooldown = 1.0f;
    private float lastHeal;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player") { 
            if(Time.time - lastHeal > healCooldown)
            {
                lastHeal = Time.time;
                GameManager.gameManagerIstance.player.Heal(healingAmount);
            }
        }
    }
}
