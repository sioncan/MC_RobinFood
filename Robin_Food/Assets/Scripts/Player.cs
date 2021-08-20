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
        DontDestroyOnLoad(gameObject);
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

    public void Heal(int healingAmount)
    {
        if (hitpoint == max_hitpoint) // se ok player ha vita max, non viene curato
            return;
        hitpoint += healingAmount;
        if(hitpoint > max_hitpoint)
            hitpoint = max_hitpoint;
        GameManager.gameManagerIstance.ShowText("+ " + healingAmount.ToString() + "hp", 20, Color.green, transform.position, Vector3.up * 30, 1.0f);
    }
}
