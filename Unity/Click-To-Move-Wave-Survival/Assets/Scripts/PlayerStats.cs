using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    public float playerMovementSpeed;
    
    [Tooltip("Attacks per second (1 = 1APS, 1.5 = 1.5APS")]
    public float playerAttackRate;
    public float playerAttackRange;
    public float playerAgroRange;
    public int playerDamage;
    
    public int playerHealth;
    
}
