using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleScript : MonoBehaviour
{
    [SerializeField] private float scaleMinForce, scaleMaxForce;
    [SerializeField] private Rigidbody2D scaleRigidbody;

    void Start()
    {
        OnScaleSpawn();
    }

    private void OnScaleSpawn()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomForceMagnitude = Random.Range(scaleMinForce, scaleMaxForce);
        scaleRigidbody.AddForce(randomDirection * randomForceMagnitude, ForceMode2D.Impulse);
    }
}
