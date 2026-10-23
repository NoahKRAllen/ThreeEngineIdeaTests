using UnityEngine;

namespace PlayerControls
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] float playerMovementSpeed;
        public float GetPlayerMovementSpeed(GameObject requester)
        {
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
            //All to stop cheaters, at least until we go fully serverside controls
            if (requester != null)
            {
                return playerAttackRange;
            }

            return 0;
        }
    
        [SerializeField] float playerAggroRange;
        public float GetPlayerAggroRange(GameObject requester)
        {
            if (requester != null)
            {
                return playerAggroRange;
            }

            return 0;
        }
    
        [SerializeField] float playerDamage;
        public float GetPlayerDamage(GameObject requester)
        {
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
    
        [SerializeField] private PlayerMovementController playerMovementController;
        public PlayerMovementController GetPlayerMovementController(GameObject requester)
        {
            if (requester != null)
            {
                return playerMovementController;
            }

            return null;
        }
        
        [SerializeField] private PlayerAttackController playerAttackController;

        
    }
}
