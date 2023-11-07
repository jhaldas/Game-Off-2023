using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleScript : MonoBehaviour
{
    [SerializeField] private float scaleMinForce = 0;
    [SerializeField] private float scaleMaxForce = 10;
    [SerializeField] private float scaleRadius = 10;
    [SerializeField] private float scaleSpeed = 5;
    [SerializeField] private float scaleValue = 10;
    [SerializeField] private Rigidbody2D scaleRigidbody;
    private float distanceToPlayer;
    private Vector2 directionToPlayer;

    void Start()
    {
        OnScaleSpawn();
    }

    void Update()
    {
        ScaleMoveToPlayerUpdate();
    }

    /// <summary>
    /// Called when scales are spawned to push them in a random direction.
    /// </summary>
    private void OnScaleSpawn()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomForceMagnitude = Random.Range(scaleMinForce, scaleMaxForce);
        scaleRigidbody.AddForce(randomDirection * randomForceMagnitude, ForceMode2D.Impulse);
    }

    private void ScaleMoveToPlayerUpdate()
    {
        if(PlayerBaseScript.playerInstance != null)
        {
            distanceToPlayer = Vector2.Distance(PlayerBaseScript.playerInstance.gameObject.transform.position, gameObject.transform.position);
            if(distanceToPlayer <= scaleRadius)
            {
                directionToPlayer = (Vector2)PlayerBaseScript.playerInstance.gameObject.transform.position - (Vector2)gameObject.transform.position;
                scaleRigidbody.velocity += directionToPlayer * scaleSpeed * Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Player")
        {
            PlayerBaseScript.playerInstance.ChangePlayerScales(scaleValue, PlayerBaseScript.PlayerValueOptions.Add);
            Destroy(gameObject);
        }
    }
}
