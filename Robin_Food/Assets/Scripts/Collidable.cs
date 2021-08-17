using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    // Filtro per sapere con cosa devo collidere
    public ContactFilter2D filter;
    // Contiene tutti gli oggetti con cui ho colliso durante il frame
    private Collider2D[] hits = new Collider2D[10];
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // rilevo collisioni
        boxCollider.OverlapCollider(filter, hits);
        // itero le collisioni rilevate, applico l'azione in base alla collisione e pulisco l'array delle collisioni
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);

            hits[i] = null;
        }
    }

    // azione in base all'oggetto con cui collido
    protected virtual void OnCollide(Collider2D coll)
    {

    }
}
