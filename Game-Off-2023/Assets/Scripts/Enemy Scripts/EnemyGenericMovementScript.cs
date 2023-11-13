using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenericMovementScript : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D enemyRigidbody;
    [SerializeField] private float enemySpeed = 5f;
    [SerializeField] private float enemyMaxSpeed = 10f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    
}
/**

SimpleEnemyMovement();

private void SimpleEnemyMovement()
    {
        if (!(enemyRigidbody.velocity.magnitude >= enemyMaxSpeed))
        {
            enemyRigidbody.velocity += EnemyToPlayerDirection() * enemySpeed * Time.deltaTime;
        }
        else 
        {
            enemyRigidbody.velocity = enemyRigidbody.velocity;
        }
    }

    private Vector2 EnemyToPlayerDirection()
    {
        return PlayerBaseScript.playerInstance.gameObject.transform.position - gameObject.transform.position;
    }


public float speed = 2f;
    public float avoidanceRadius = 2f;

    private void Move(Vector2 avoidanceForce)
    {
        // Move the AI with avoidance force
        enemyRigidbody.velocity += EnemyToPlayerDirection() * avoidanceForce * ( speed * Time.deltaTime);
    }

    private Vector2 AvoidObstacles()
    {
        // Cast rays in a fan shape to detect obstacles
        float rayAngle = 30f;
        int rayCount = 5;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * rayAngle - (rayCount - 1) * rayAngle / 2;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.up;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, avoidanceRadius);

            // If an obstacle is detected, calculate avoidance force
            if (hit.collider != null)
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);
                return direction * speed;
            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + (Vector3)direction * avoidanceRadius, Color.green);
            }
        }

        return Vector2.zero;
    }
**/