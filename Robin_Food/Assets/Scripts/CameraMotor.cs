using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{

    public Transform lookAt;
    // Offset spostamento player prima che la camera si sposta per inseguirlo
    public float boundX;
    public float boundY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // LateUpdate is once per frame after Update and FixedUpdate
    public void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        // Controlla se siamo nei limiti dello spostamento orizzontale
        float deltaX = lookAt.position.x - transform.position.x;
        if(deltaX > boundX || deltaX < -boundX)
        {
            if(transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            } else
            {
                delta.x = deltaX + boundX;
            }
        }

        // Controlla se siamo nei limiti dello spostamento verticale
        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        // Spostiamo la camera
        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
