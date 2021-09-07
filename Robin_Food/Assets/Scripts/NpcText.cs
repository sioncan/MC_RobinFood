using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcText : Collidable
{
    public string msg;
    private readonly float cooldown = 5.1f;
    private float lastSpeak = -5.1f; // cosi puo parlare da subito appena inizia il gioco
    private Vector3 textUp = new Vector3(0, 0.16f, 0);

    protected override void OnCollide(Collider2D coll)
    {
        if (Time.time - lastSpeak > cooldown)
        {
            lastSpeak = Time.time;
            GameManager.gameManagerIstance.ShowText(msg, 30, Color.white, transform.position + textUp, Vector3.zero, 5.0f);
        }
    }
}
