using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFishShootingScript : MonoBehaviour
{

    [SerializeField] private GameObject fishBulletPrefab;

    void Start()
    {
        //Instantiate(fishBulletPrefab, transform.position, Quaternion.Euler(0,0,0));
    }
}
