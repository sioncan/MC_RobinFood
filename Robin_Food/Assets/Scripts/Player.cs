using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Mover
{
    private CharacterController controller;
    public Transform leftJoystick;

    private bool isAlive = true;
    private SpriteRenderer spriteRenderer;
    public RuntimeAnimatorController[] controllers;

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
        GameManager.gameManagerIstance.PauseGame();
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

    // Al cambiare della skin/sprite, cambio anche l'animator controller (ce ne è uno per ogni skin)
    public void SwapSprite(int skinId)
    {
        animator.runtimeAnimatorController = controllers[skinId];
        spriteRenderer.sprite = GameManager.gameManagerIstance.playerSprites[skinId];
    }

    public void OnLevelUp()
    {
        maxHitpoint += 10;
        hitpoint = maxHitpoint;
        GameManager.gameManagerIstance.ShowText("Level up !", 30, Color.red, transform.position, Vector3.up * 30, 1.0f);
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
        GameManager.gameManagerIstance.ShowText("+ " + healingAmount.ToString() + "hp", 30, Color.green, transform.position, Vector3.up * 30, 1.0f);
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
