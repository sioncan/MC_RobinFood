using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : Collidable
{
    public int healingAmount = 1;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
