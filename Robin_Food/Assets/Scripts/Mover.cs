using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter  
{
    protected CapsuleCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float xspeed = 1.0f;
    protected float yspeed = 0.75f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<CapsuleCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector3(input.x * xspeed, input.y * yspeed, 0);

        // Giro il Player in base alla direzione in cui vado
        if (moveDelta.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // aggiunge la pushForce come vettore
        moveDelta += pushDirection;
        // riduce la pushForce ogni frame, in base alla recoverySpeed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        // Muovo il Player in base all'input moveDelta
        transform.Translate(moveDelta * Time.deltaTime);
    }
        
}
