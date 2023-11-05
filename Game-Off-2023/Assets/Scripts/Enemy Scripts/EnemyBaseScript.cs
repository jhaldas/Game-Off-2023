using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseScript : MonoBehaviour, IOnHit
{

    [SerializeField] private float enemyHealth = 100;
    [SerializeField] private float enemyMaxHealth = 100;
    public Rigidbody2D enemyRigidbody;

    void Start()
    {
        
    }

    void Update()
    {
        EnemyDeathUpdate();
    }

    public void OnHealthChange(float damage)
    {
        enemyHealth += damage;
    }

    public void OnKnockback(float knockback, Vector2 direction)
    {
        enemyRigidbody.AddForce(direction * knockback);
    }

    private void EnemyDeathUpdate()
    {
        if(enemyHealth<=0)
        {
            Destroy(gameObject);
        }
    }
}
