using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2 : Enemy
{
    public GameObject duplicatePrefab;
    private int duplicateNum;
    public int duplicateMaxNum = 3;
    public float duplicateCooldown = 15.0f;
    public float lastDuplicate;
    private int coinsAmount = 100;

    // Update is called once per frame
    private void Update()
    {   // crea una copia di se stesso ogni cooldown solo se è vivo e se non ma vita max (dal momento in cui viene colpito)
        if (isAlive && Time.time - lastDuplicate > duplicateCooldown && hitpoint < maxHitpoint && duplicateNum < duplicateMaxNum && GameManager.isPaused == false)
        {
            // ogni cooldown duplico il boss
            Instantiate(duplicatePrefab, new Vector3(transform.position.x + 0.5f, transform.position.y, 0), Quaternion.identity);
            lastDuplicate = Time.time;
            duplicateNum++;
        }
    }
    protected override void Death()
    {
        base.Death();
        GameManager.gameManagerIstance.coins += coinsAmount;
    }
}

