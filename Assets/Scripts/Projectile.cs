using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date): 07-12-2024
 */
public class Projectile : MonoBehaviour
{
    //Default speed values for projectiles
    public float speedX = 3.0f;
    public float speedY = 0.0f;
    public Vector2 moveSpeed;
    public int damage = 10;
    public int lifespan = 5; //How long the projectile lasts before despawning
    private float timeElapsed = 0.0f;
    public Vector2 knockback = Vector2.zero;

    public bool CanMove { get { return anim.GetBool(AnimationStrings.canMove); } }

    //Object components
    Rigidbody2D rigidbod;
    Animator anim;
    CapsuleCollider2D hitBox;

    private void Awake()
    {
        rigidbod = GetComponent<Rigidbody2D>();
        moveSpeed = new Vector2(speedX, speedY);
        anim = GetComponent<Animator>();
        hitBox = GetComponent<CapsuleCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbod.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    //On collision with an entity that can take damage, deal damage + knockback
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            anim.SetBool(AnimationStrings.canMove, false);
            Vector2 directionalKnock = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            hitBox.enabled = false; //Prevent the spell from damaging anyone else
            damageable.Hit(damage, directionalKnock);
            anim.SetTrigger(AnimationStrings.fireboltDie);
        }
    }

    //When the timeElasped matches the lifespawn, destroy the projectile
    private void Update()
    {
        if (!CanMove) { rigidbod.velocity = new Vector2(0, 0); } //If it cannot move, set the speed to zero
        timeElapsed += Time.deltaTime;
        if(timeElapsed > lifespan) { anim.SetTrigger(AnimationStrings.fireboltDie); }
    }
}