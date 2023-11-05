using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordSlashScript : MonoBehaviour
{
    [SerializeField] private float slashRange = 100;
    [SerializeField] private float slashDamage = 100;
    [SerializeField] private float slashShotspeed = 1;
    private float slashTimer = 10f;
    
    void Update()
    {
        if(slashTimer <= 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * slashShotspeed * Time.deltaTime);
        slashTimer -= Time.deltaTime;
    }

    /// <summary>
    /// Set the slash's value
    /// </summary>
    /// <param name="range">Range float</param>
    /// <param name="damage">Damage float</param>
    /// <param name="shotspeed">Shotspeed float</param>
    public void SetSlashValues(float range, float damage, float shotspeed)
    {
        slashTimer = range;
        slashRange = range;
        slashDamage = damage;
        slashShotspeed = shotspeed;
    }

    private void PrintslashStats()
    {
        Debug.Log("Slash Range: " + slashRange);
        Debug.Log("Slash Damage: " + slashDamage);
        Debug.Log("Slash Shotspeed: " + slashShotspeed);
    }
}
