using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField] private float bulletRange = 100;
    [SerializeField] private float bulletDamage = 100;
    [SerializeField] private float bulletShotspeed = 1;
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
    public void SetBulletValues(float range, float damage, float shotspeed)
    {
        bulletTimer = range;
        bulletRange = range;
        bulletDamage = damage;
        bulletShotspeed = shotspeed;
    }

    private void PrintBulletStats()
    {
        Debug.Log("Bullet Range: " + bulletRange);
        Debug.Log("Bullet Damage: " + bulletDamage);
        Debug.Log("Bullet Shotspeed: " + bulletShotspeed);
    }
}
/**
void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name != "Player")
        {
            healthScript health = collider.gameObject.GetComponent<healthScript>();
            if (health != null)
            {
                health.OnHealthChange(-bulletDamage);
                Destroy(gameObject);
            }
            if (collider.gameObject.tag == "Wall")
            {
                Destroy(gameObject);
            }
        }
    }
**/
