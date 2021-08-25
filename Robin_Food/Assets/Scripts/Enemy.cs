using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public bool isAlive;
    public int xpValue = 1;
    public float triggerLength = 0.3f;
    public float chaseLength = 1.0f;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;
    public ContactFilter2D filter;
    private CapsuleCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        isAlive = true;
        playerTransform = GameManager.gameManagerIstance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // controllo se il player è nella distanza di aggro
        if(Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if(Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
                chasing = true;

            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            } else
            {   // se il player non è più nel range di aggro, l'npc torna al punto di spawn
                UpdateMotor(startingPosition - transform.position); 
            }
        } else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }
        // controlla overlaps
        collidingWithPlayer = false;
        // rilevo collisioni
        boxCollider.OverlapCollider(filter, hits);
        // itero le collisioni rilevate, applico l'azione in base alla collisione e pulisco l'array delle collisioni
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if (hits[i].tag == "Player" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            hits[i] = null;
        }
    }

    protected override void Death()
    {
        isAlive = false;
        Destroy(gameObject);
        GameManager.gameManagerIstance.GrantXp(xpValue);
    }
}
