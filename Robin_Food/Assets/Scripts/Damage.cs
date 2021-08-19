using UnityEngine;

// serve per comunicare il danno all'NPC danneggiato dal player
public struct Damage
{
    public Vector3 origin;
    public int damageAmount;
    public float pushForce;
}
