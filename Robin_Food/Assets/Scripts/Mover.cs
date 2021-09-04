using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter  
{
    protected CapsuleCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    public float xspeed = 1.0f;
    public float yspeed = 1.0f;
    protected Vector3 originalSize;
    protected Animator animator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        originalSize = transform.localScale; 
        boxCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector3(input.x * xspeed, input.y * yspeed, 0);
        animator.enabled = true;

        // Giro il Player in base alla direzione in cui vado
        if (moveDelta.x > 0)
            transform.localScale = originalSize;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);
        else
            animator.enabled = false;

        // aggiunge la pushForce come vettore
        moveDelta += pushDirection;
        // riduce la pushForce ogni frame, in base alla recoverySpeed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        // Muovo il Player in base all'input moveDelta
        transform.Translate(moveDelta * Time.deltaTime);
    }
        
}
