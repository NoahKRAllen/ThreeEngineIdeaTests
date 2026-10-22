using UnityEngine;

public class PlayerStats : MonoBehaviour
{
//Stopping use of PlayerStats currently, will come back to this and make it a child class of UnitStats if there are any extra
//stats that the player must have that not all units share
    [SerializeField] float playerMovementSpeed;
    public float GetPlayerMovementSpeed(GameObject requester)
    {
        //This is to stop any external script from accessing values
        if (requester != null)
        {
            return playerMovementSpeed;
        }

        return 0;
    }
    
    [Tooltip("Attacks per second (1 = 1APS, 1.5 = 1.5APS")]
    [SerializeField] float playerAttackRate;
    public float GetPlayerAttackRate(GameObject requester)
    {
        if (requester != null)
        {
            return playerAttackRate;
        }
        return 0;
    }
    
    [SerializeField] float playerAttackRange;
    public float GetPlayerAttackRange(GameObject requester)
    {
        //This is to stop any external script from accessing values
        //This will need to be changed to actually check if the requester has the required tag/active script to make these calls
        if (requester != null)
        {
            return playerAttackRange;
        }

        return 0;
    }
    
    [SerializeField] float playerAgroRange;
    public float GetPlayerAgroRange(GameObject requester)
    {
        //This is to stop any external script from accessing values
        if (requester != null)
        {
            return playerAgroRange;
        }

        return 0;
    }
    
    [SerializeField] float playerDamage;
    public float GetPlayerDamage(GameObject requester)
    {
        //This is to stop any external script from accessing values
        if (requester != null)
        {
            return playerDamage;
        }

        return 0;
    }
    public void SetPlayerDamage(GameObject requester, float value)
    {
        if (requester != null)
        {
            playerDamage = value;
        }
    }
    
    [SerializeField] float playerHealth;
    public float GetPlayerHealth(GameObject requester)
    {
        //This is to stop any external script from accessing values
        if (requester != null)
        {
            return playerHealth;
        }

        return 0;
    }

    public void SetPlayerHealth(GameObject requester, int value)
    {
        if (requester != null)
        {
            playerHealth = value;
        }
    }
    
    
    
}
