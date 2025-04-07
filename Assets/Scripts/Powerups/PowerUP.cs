using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * Date Created: 06-04-2025 | Last Modified: 06-04-2025 
 * 
 * This script is used as a base template for powerups to apply movement speed buffs
 */
public class PowerUP : MonoBehaviour
{
    [SerializeField] List<PowerUpEffect> powerUpEffects;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsule;
    //The original position of the object in the scene
    Vector2 origin;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsule = GetComponent<CapsuleCollider2D>();
        origin = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            StartCoroutine(ApplyPowerUp(collision.gameObject));
            spriteRenderer.enabled = false;
            capsule.enabled = false;
        }
    }

    private IEnumerator ApplyPowerUp(GameObject target)
    {
        ApplyEffects(target);
        yield return new WaitForSeconds(5f);
        RemoveEffects(target);
        Destroy(gameObject);
    }

    public void ApplyEffects(GameObject player)
    {
        foreach (PowerUpEffect effect in powerUpEffects)
        {
            effect.Apply(player);
        }
    }

    public void RemoveEffects(GameObject player)
    {
        foreach (PowerUpEffect effect in powerUpEffects)
        {
            effect.Remove(player);
        }
    }

    //This method allows the item to bob up and down while remaining in the same x position
    private void Update()
    {
        float newY = Mathf.Sin(Time.time * 2f) * 0.25f + origin.y;
        transform.position = new Vector2(origin.x, newY);
    }

}