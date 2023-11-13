using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFishBulletScript : MonoBehaviour
{
    public Transform targetObject;
    public float speed = 2.0f;
    public float distance = 3.0f;

    private Vector2 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        targetObject = PlayerBaseScript.playerInstance.transform;
    }

    void Update()
    {
        float xOffset = Mathf.Sin(Time.time * speed) * distance;
        Vector2 newPosition = new Vector2(targetObject.position.x, initialPosition.y + xOffset);

        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
    }
}
