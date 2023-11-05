using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrenadeScript : MonoBehaviour
{
    [SerializeField] private float grenadeRange = 100;
    [SerializeField] private float grenadeDamage = 100;
    [SerializeField] private float grenadeShotspeed = 3;
    private float grenadeTimer = 2f;
    private float grenadeExplosionTimer = 2f; // How long the explosion damage radius lasts
    [SerializeField] private GameObject grenadeExplosionRadius;
    
    void Update()
    {
        if(grenadeTimer <= 0)
        {
            if(!grenadeExplosionRadius.activeSelf)
            {
                grenadeExplosionRadius.SetActive(true);
            }
            if(grenadeExplosionTimer > 0)
            {
                grenadeExplosionTimer -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.Translate(Vector2.right * grenadeShotspeed * Time.deltaTime);
            grenadeTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Set the grenade's value
    /// </summary>
    /// <param name="range">Range float</param>
    /// <param name="damage">Damage float</param>
    /// <param name="shotspeed">Shotspeed float</param>
    public void SetGrenadeValues(float range, float damage, float shotspeed)
    {
        grenadeTimer = range;
        grenadeRange = range;
        grenadeDamage = damage;
        grenadeShotspeed = shotspeed;
    }

    private void PrintGrenadeStats()
    {
        Debug.Log("Grenade Range: " + grenadeRange);
        Debug.Log("Grenade Damage: " + grenadeDamage);
        Debug.Log("Grenade Shotspeed: " + grenadeShotspeed);
    }
}
