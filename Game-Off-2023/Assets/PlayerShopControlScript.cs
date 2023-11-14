using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShopControlScript : MonoBehaviour
{

    private ShopController shopController;
    // Start is called before the first frame update
    void Start()
    {
        shopController = FindObjectOfType<ShopController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerShop() 
    {
        if (shopController.PlayerInShopRadius() && !shopController.PlayerShopUIEnabled())
        {
            shopController.EnableMenu();
            return;
        }
        shopController.DisableMenu();
    }
}
