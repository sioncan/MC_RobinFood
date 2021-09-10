using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1 : Enemy
{
    public GameObject companionPrefab;
    private GameObject[] companions;
    private int companionNum = 4;
    public float compSpeed = 0.5f;
    public float attackCooldown = 3.0f;
    public float lastAttack;
    private int coinsAmount = 100;

    public void Awake()
    {   // genero i companions e li metto intorno al boss come punto di partenza
        companions = new GameObject[companionNum];
        companions[0] = Instantiate(companionPrefab, new Vector3(transform.position.x + 0.1f, transform.position.y, 0), Quaternion.identity);
        companions[1] = Instantiate(companionPrefab, new Vector3(transform.position.x - 0.1f, transform.position.y, 0), Quaternion.identity);
        companions[2] = Instantiate(companionPrefab, new Vector3(transform.position.x, transform.position.y + 0.1f, 0), Quaternion.identity);
        companions[3] = Instantiate(companionPrefab, new Vector3(transform.position.x, transform.position.y - 0.1f, 0), Quaternion.identity);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAlive && GameManager.isPaused == false && chasing)
        {
            if (Time.time - lastAttack < attackCooldown)
            {   // ogni cooldown spara i companions nelle 4 direzioni (N,S,E,W)
                companions[0].GetComponent<Rigidbody2D>().velocity = new Vector3(1 * compSpeed, 0, 0);
                companions[1].GetComponent<Rigidbody2D>().velocity = new Vector3(-1 * compSpeed, 0, 0);
                companions[2].GetComponent<Rigidbody2D>().velocity = new Vector3(0, 1 * compSpeed, 0);
                companions[3].GetComponent<Rigidbody2D>().velocity = new Vector3(0, -1 * compSpeed, 0);
            }
            else
            {   // ogni cooldown elimina i companions e li rigenera in posizione iniziale
                for (int i = 0; i < companions.Length; i++)
                {
                    Destroy(companions[i]);
                }
                companions[0] = Instantiate(companionPrefab, new Vector3(transform.position.x + 0.1f, transform.position.y, 0), Quaternion.identity);
                companions[1] = Instantiate(companionPrefab, new Vector3(transform.position.x - 0.1f, transform.position.y, 0), Quaternion.identity);
                companions[2] = Instantiate(companionPrefab, new Vector3(transform.position.x, transform.position.y + 0.1f, 0), Quaternion.identity);
                companions[3] = Instantiate(companionPrefab, new Vector3(transform.position.x, transform.position.y - 0.1f, 0), Quaternion.identity);
                GameObject.Find("Player").transform.Translate(Vector3.zero);
                lastAttack = Time.time;
                GetComponent<AudioSource>().Play();
            }
        }
    }

    protected override void Death()
    {
        base.Death();
        for (int i = 0; i < companions.Length; i++)
        {
            Destroy(companions[i]);
            GameObject.Find("Player").transform.Translate(Vector3.zero);
            GameManager.gameManagerIstance.coins += coinsAmount;
        }
    }
}
