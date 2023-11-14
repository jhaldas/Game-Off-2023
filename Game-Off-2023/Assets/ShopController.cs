using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    private PlayerWeaponScript playerWeaponScript;

    private PlayerWeaponDictionary playerWeapons;

    public GameObject[] shopOptions;

    [SerializeField] private GameObject shopUI;

    [SerializeField] private GameObject shopOptionsParent;

    private bool playerInRange = false;

    [SerializeField] private GameObject interactPanel;

    // Start is called before the first frame update
    void Start()
    {
        playerWeaponScript = PlayerBaseScript.playerInstance.GetComponent<PlayerWeaponScript>();
        shopOptions = new GameObject[6];

        int index = 0;
        foreach (Transform child in shopOptionsParent.transform) 
        {
            shopOptions[index] = child.gameObject;
            index++;
        }
    }

    public void RefreshShopOptions() 
    {
        bool[] unlockedWeapons = playerWeaponScript.GetUnlockedWeapon();
        int index = 0;
        foreach (Transform option in shopOptionsParent.transform)
        {
            if (unlockedWeapons[index] == true) 
            {
                option.Find("Locked").gameObject.SetActive(false);
                option.Find("Unlocked").gameObject.SetActive(true);
            }
            index++;
        }
    }

    public void EnableMenu()
    {
        RefreshShopOptions();
        shopUI.SetActive(true);
    }

    public void DisableMenu()
    {
        RefreshShopOptions();
        shopUI.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            interactPanel.SetActive(true);
            playerInRange = true;
        }    
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            interactPanel.SetActive(false);
            playerInRange = false;
        }
    }

    public bool PlayerInShopRadius() 
    {
        return playerInRange;
    }

    public bool PlayerShopUIEnabled()
    {
        return shopUI.activeSelf;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
