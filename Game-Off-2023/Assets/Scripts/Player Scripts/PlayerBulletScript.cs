using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField] private float bulletRange = 100;
    [SerializeField] private float bulletDamage = 100;
    [SerializeField] private float bulletShotSpeed = 1;
    
    void Update()
    {
        if(bulletRange <= 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * bulletShotSpeed * Time.deltaTime);
        bulletRange -= Time.deltaTime;
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
