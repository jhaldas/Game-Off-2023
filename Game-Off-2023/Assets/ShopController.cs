using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    private PlayerWeaponScript playerWeaponScript;

    private PlayerWeaponDictionary playerWeapons;

    public GameObject[] shopOptions;

    // Start is called before the first frame update
    void Start()
    {
        playerWeaponScript = PlayerBaseScript.playerInstance.GetComponent<PlayerWeaponScript>();
        shopOptions = new GameObject[6];

        int index = 0;
        foreach (Transform child in transform) 
        {
            shopOptions[index] = child.gameObject;
            index++;
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
