using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Mover
{
    private CharacterController controller;

    private bool isAlive = true;
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<CharacterController>();
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive)
            return;
        base.ReceiveDamage(dmg);
        GameManager.gameManagerIstance.OnHitpointChange();
    }
    protected override void Death()
    {
        isAlive = false;
        GameManager.gameManagerIstance.deathMenuAnim.SetTrigger("Show");
    }

    private void FixedUpdate()
    {
        // L'input per muoversi simula il joystick sinistro di un gamepad
        // passo i valori del joystick al motore
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.
        UpdateMotor(gamepad.leftStick.ReadValue());  
    }

    public void SwapSprite(int skindId)
    {
        spriteRenderer.sprite = GameManager.gameManagerIstance.playerSprites[skindId];
    }

    public void OnLevelUp()
    {
        maxHitpoint++;
        hitpoint = maxHitpoint;
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
        if (hitpoint == maxHitpoint) // se ok player ha vita max, non viene curato
            return;
        hitpoint += healingAmount;
        if(hitpoint > maxHitpoint)
            hitpoint = maxHitpoint;
        GameManager.gameManagerIstance.ShowText("+ " + healingAmount.ToString() + "hp", 20, Color.green, transform.position, Vector3.up * 30, 1.0f);
        GameManager.gameManagerIstance.OnHitpointChange();
    }

    public void Respawn()
    {
        Heal(maxHitpoint);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }
}
