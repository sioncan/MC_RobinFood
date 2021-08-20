using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        // Prendo gli input WASD e li metto nel vettore moveDelta
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));  
    }

    public void SwapSprite(int skindId)
    {
        spriteRenderer.sprite = GameManager.gameManagerIstance.playerSprites[skindId];
    }

    public void OnLevelUp()
    {
        max_hitpoint++;
        hitpoint = max_hitpoint;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }
}
