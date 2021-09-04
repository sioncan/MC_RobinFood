using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_0 : Enemy
{
    public Transform[] companions;
    public float[] compSpeed = { 2.5f, -2.5f };
    public float compDistance = 0.5f;
    private int coinsAmount = 100;

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.isPaused == false)
        {
            for (int i = 0; i < companions.Length; i++)
            {
                companions[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * compSpeed[i]) * compDistance, Mathf.Sin(Time.time * compSpeed[i]) * compDistance, 0);
            }
        }
    }

    protected override void Death()
    {
        base.Death();
        GameManager.gameManagerIstance.coins += coinsAmount;
    }
}
