using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseScript : MonoBehaviour, IOnHit
{

    [SerializeField] private float enemyHealth = 100;
    [SerializeField] private float enemyMaxHealth = 100;
    [SerializeField] private Rigidbody2D enemyRigidbody;
    [SerializeField] private float scaleDropAmount = 10;
    [SerializeField] private float xpDropAmount = 10;
    public GameObject scalePrefab;

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
        enemyRigidbody.AddForce(direction * knockback, ForceMode2D.Impulse);
    }

    private void EnemyDeathUpdate()
    {
        if(enemyHealth<=0)
        {
            PlayerBaseScript.playerInstance.ChangePlayerXP(xpDropAmount, PlayerBaseScript.PlayerValueOptions.Add);
            OnEnemyDeath();
        }
    }

    private void OnEnemyDeath()
    {
        for(int i = 0; i < scaleDropAmount; i++)
        {
            Instantiate(scalePrefab,transform.position,transform.rotation);
        }
        Destroy(gameObject);
    }
}
