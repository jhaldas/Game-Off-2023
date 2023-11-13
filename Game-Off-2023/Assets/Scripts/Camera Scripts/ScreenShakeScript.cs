using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeScript : MonoBehaviour
{
    public static ScreenShakeScript screenShake;
    [SerializeField] private float screenShakeSpeed = 1.5f;
    [SerializeField] private float currentShakeDuration = 0f;
    [SerializeField] private float currentShakeMagnitude = 0f;
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private bool enableScreenShake = true;

    void Awake()
    {
        if(screenShake == null)
        {
            screenShake = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if(enableScreenShake)
        {
            ScreenShakeUpdate();
        }
    }

    private void ScreenShakeUpdate()
    {
        if (currentShakeDuration > 0)
        {
            transform.localPosition = originalPosition + Random.insideUnitSphere * currentShakeMagnitude;

            currentShakeDuration -= Time.deltaTime * screenShakeSpeed;
        }
        else
        {
            currentShakeDuration = 0f;
            currentShakeMagnitude = 0f;
            transform.localPosition = originalPosition;
        }
    }

    public void AddShake(float duration, float magnitude)
    {
        currentShakeDuration += duration;
        currentShakeMagnitude += magnitude/100;
    }
}
