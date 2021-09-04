using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public Player player;
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player") 
        {
            // Ogni volta che il player attraversa il portale, salviamo il gioco
            GameManager.gameManagerIstance.SaveState();
            // Se collide con il Player, lo teleportiamo nel Dungeon
            SceneManager.LoadScene("Dungeon_1");
        }
    }
}
