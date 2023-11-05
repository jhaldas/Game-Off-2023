using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField] private float bulletRange = 100;
    [SerializeField] private float bulletDamage = 100;
    [SerializeField] private float bulletShotspeed = 1;
    [SerializeField] private float bulletKnockback = 1;
    private float bulletTimer = 10f;
    
    void Update()
    {
        if(bulletTimer <= 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * bulletShotspeed * Time.deltaTime);
        bulletTimer -= Time.deltaTime;
    }

    /// <summary>
    /// Set the bullet's value
    /// </summary>
    /// <param name="range">Range float</param>
    /// <param name="damage">Damage float</param>
    /// <param name="shotspeed">Shotspeed float</param>
    public void SetBulletValues(float range, float damage, float shotspeed, float knockback)
    {
        bulletTimer = range;
        bulletRange = range;
        bulletDamage = damage;
        bulletShotspeed = shotspeed;
        bulletKnockback = knockback;
    }

    private void PrintBulletStats()
    {
        Debug.Log("Bullet Range: " + bulletRange);
        Debug.Log("Bullet Damage: " + bulletDamage);
        Debug.Log("Bullet Shotspeed: " + bulletShotspeed);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name != "Player")
        {
            IOnHit onHit = collider.gameObject.GetComponent<IOnHit>();
            if (onHit != null)
            {
                onHit.OnHealthChange(-bulletDamage);
                onHit.OnKnockback(bulletKnockback, HandleDirectionOfImpact(collider.gameObject.transform.position));
                Destroy(gameObject);
            }
            if (collider.gameObject.tag == "Wall")
            {
                Destroy(gameObject);
            }
        }
    }

    private Vector2 HandleDirectionOfImpact(Vector2 enemy)
    {
        Vector2 direction = enemy - (Vector2)transform.position;
        direction.Normalize();
        return direction;
    }
}
/**

**/
