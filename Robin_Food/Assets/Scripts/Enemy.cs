using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public bool isAlive;
    public int xpValue = 1;
    private int coinsAmount = 1;
    public float triggerLength = 0.3f;
    public float chaseLength = 1.0f;
    protected bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;
    public ContactFilter2D filter;
    private CapsuleCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[100];
    public Animator hitEffect;
    public AudioSource deathSound;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        isAlive = true;
        playerTransform = GameManager.gameManagerIstance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<CapsuleCollider2D>();
        deathSound = GameObject.Find("DeathSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (GameManager.isPaused == false)
        {
            // controllo se il player � nella distanza di aggro
            if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
            {
                if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
                    chasing = true;

                if (chasing)
                {
                    if (!collidingWithPlayer)
                    {
                        UpdateMotor((playerTransform.position - transform.position).normalized);
                    }
                    else
                    {
                        UpdateMotor(Vector3.zero);
                    }
                }
                else
                {   // se il player non � pi� nel range di aggro, l'npc torna al punto di spawn
                    UpdateMotor(startingPosition - transform.position);
                }
            }
            else
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

                if (hits[i].tag == "Player" || hits[i].tag == "Fighter")
                {
                    collidingWithPlayer = true;
                }

                hits[i] = null;
            }
        }
    }

    protected override void Death()
    {
        deathSound.Play();
        isAlive = false;
        Destroy(gameObject);
        GameManager.gameManagerIstance.GrantXp(xpValue);
        GameManager.gameManagerIstance.coins += coinsAmount;
    }
}
