using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // damage struct
    public int damagePoint = 1;
    public float pushForce = 4.0f;
    // swing
    private Animator animator;
    private float cooldown = 0.5f;
    private float lastSwing;
    // upgrade
    public int weaponLevel;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }
    // metodo chiamato quando si attacca, attiviamo il trigger dell'animator/animation
    private void Swing()
    {
        animator.SetTrigger("swing");
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if (coll.name == "Player")
                return;

            // creiamo un oggetto Damage, e lo mandiamo all'npc colpito
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}
