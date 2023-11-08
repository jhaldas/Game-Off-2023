using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBaseScript : MonoBehaviour, IOnHit
{
    public static PlayerBaseScript playerInstance;
    [SerializeField] private float playerLevel = 1;
    [SerializeField] private float playerSkillPoints = 0;
    [SerializeField] private float playerXP = 0;
    [SerializeField] private float playerXPCap = 100;
    [SerializeField] private float playerScales = 0;
    [SerializeField] private float playerHealth = 100;
    [SerializeField] private float playerMaxHealth = 100;
    [SerializeField] private Rigidbody2D playerRigidbody;
    public TextMeshProUGUI[] playerStatsText;
    
    public enum PlayerValueOptions
    {
        Set         = 0,
        Add         = 1,
        Multiply    = 2
    };

    void Awake()
    {
        if(playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        InitializePlayerStatsUI();
    }
    
    void Update()
    {
        PlayerHealthUpdate();
        PlayerXPUpdate();
    }

    public void OnHealthChange(float damage)
    {
        playerHealth += damage;
        OnPlayerStatsUIChange(0);
    }

    public void OnKnockback(float knockback, Vector2 direction)
    {
        playerRigidbody.AddForce(direction * knockback, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Checks for when the player is dead
    /// </summary>
    private void PlayerHealthUpdate()
    {
        if(playerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void PlayerXPUpdate()
    {
        if(playerXP >= playerXPCap)
        {
            OnPlayerLevelUp();
        }
    }

     private void OnPlayerLevelUp()
    {
        playerLevel += 1;
        OnPlayerStatsUIChange(3);
        playerSkillPoints += 1;
        float tempPlayerXPCap = playerXPCap;
        playerXPCap += 100;
        playerXP -= tempPlayerXPCap;
        OnPlayerStatsUIChange(1);
    }

    /// <summary>
    /// Change player's max health.
    /// </summary>
    /// <param name="maxHealthValue">Set the float value</param>
    /// <param name="option">Options: Set, Add, Multiply</param>
    public void ChangePlayerMaxHealth(float maxHealthValue, PlayerValueOptions option)
    {
        if(option == PlayerValueOptions.Set)
        {
            playerMaxHealth = maxHealthValue;
            OnPlayerStatsUIChange(0);
        }
        else if(option == PlayerValueOptions.Add)
        {
            playerMaxHealth += maxHealthValue;
            OnPlayerStatsUIChange(0);
        }
        else if(option == PlayerValueOptions.Multiply)
        {
            playerMaxHealth *= maxHealthValue;
            OnPlayerStatsUIChange(0);
        }
        else
        {
            Debug.Log("Option not valid.");
        }
    }

    /// <summary>
    /// Change player's health.
    /// </summary>
    /// <param name="healthValue">Set the float value</param>
    /// <param name="option">Options: Set, Add, Multiply</param>
    public void ChangePlayerHealth(float healthValue, PlayerValueOptions option)
    {
        if(option == PlayerValueOptions.Set)
        {
            playerHealth = healthValue;
            OnPlayerStatsUIChange(0);
        }
        else if(option == PlayerValueOptions.Add)
        {
            playerHealth += healthValue;
            OnPlayerStatsUIChange(0);
        }
        else if(option == PlayerValueOptions.Multiply)
        {
            playerHealth *= healthValue;
            OnPlayerStatsUIChange(0);
        }
        else
        {
            Debug.Log("Option not valid.");
        }
    }

    /// <summary>
    /// Change player's scales.
    /// </summary>
    /// <param name="scaleValue">Set the float value</param>
    /// <param name="option">Options: Set, Add, Multiply</param>
    public void ChangePlayerScales(float scaleValue, PlayerValueOptions option)
    {
        if(option == PlayerValueOptions.Set)
        {
            playerScales = scaleValue;
            OnPlayerStatsUIChange(2);
        }
        else if(option == PlayerValueOptions.Add)
        {
            playerScales += scaleValue;
            OnPlayerStatsUIChange(2);
        }
        else if(option == PlayerValueOptions.Multiply)
        {
            playerScales *= scaleValue;
            OnPlayerStatsUIChange(2);
        }
        else
        {
            Debug.Log("Option not valid.");
        }
    }

    /// <summary>
    /// Change player's xp.
    /// </summary>
    /// <param name="playerXPValue">Set the float value</param>
    /// <param name="option">Options: Set, Add, Multiply</param>
    public void ChangePlayerXP(float playerXPValue, PlayerValueOptions option)
    {
        if(option == PlayerValueOptions.Set)
        {
            playerXP = playerXPValue;
            OnPlayerStatsUIChange(1);
        }
        else if(option == PlayerValueOptions.Add)
        {
            playerXP += playerXPValue;
            OnPlayerStatsUIChange(1);
        }
        else if(option == PlayerValueOptions.Multiply)
        {
            playerXP *= playerXPValue;
            OnPlayerStatsUIChange(1);
        }
        else
        {
            Debug.Log("Option not valid.");
        }
    }

    /// <summary>
    /// Change UI when player stat changes.
    /// </summary>
    /// <param name="option">0 - Health | 1 - XP | 2 - Scales | 3 - Level </param>
    private void OnPlayerStatsUIChange(int option)
    {
        if(option == 0)
        {
            playerStatsText[option].text = "Player Health: " + playerHealth + " / " + playerMaxHealth;
        }
        if(option == 1)
        {
            playerStatsText[option].text = "Player XP: " + playerXP + " / " + playerXPCap;
        }
        if(option == 2)
        {
            playerStatsText[option].text = "Player Scales: " + playerScales;
        }
        if(option == 3)
        {
            playerStatsText[option].text = "Player Level: " + playerLevel;
        }
    }

    private void InitializePlayerStatsUI()
    {
        OnPlayerStatsUIChange(0);
        OnPlayerStatsUIChange(1);
        OnPlayerStatsUIChange(2);
        OnPlayerStatsUIChange(3);
    }
}
