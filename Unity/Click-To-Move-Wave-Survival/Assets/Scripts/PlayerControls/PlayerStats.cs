using Interfaces;
using UnityEngine;

namespace PlayerControls
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] float playerMovementSpeed;

        [Tooltip("Attacks per second (1 = 1APS, 1.5 = 1.5APS")] [SerializeField]
        float playerAttackSpeed;

        [SerializeField] float playerAttackRange;

        [SerializeField] float playerAggroRange;

        [SerializeField] int playerDamage;


        [SerializeField] int playerHealth;


        [SerializeField] private PlayerMovementController playerMovementController;

        [SerializeField] private PlayerAttackController playerAttackController;


        private bool ValidateRequest(GameObject requester)
        {
            //This will be used to handle the multiplayer logic in the future to ensure whatever is accessing code has permission to do so
            if (requester)
            {
                return true;
            }

            return false;
        }

        public float GetPlayerMovementSpeed(GameObject requester) =>
            ValidateRequest(requester) ? playerMovementSpeed : 0f;

        public float GetPlayerAttackRate(GameObject requester) =>
            ValidateRequest(requester) ? playerAttackSpeed : 0f;

        public float GetPlayerAttackRange(GameObject requester) =>
            ValidateRequest(requester) ? playerAttackRange : 0f;

        public float GetPlayerAggroRange(GameObject requester) =>
            ValidateRequest(requester) ? playerAggroRange : 0f;

        public int GetPlayerDamage(GameObject requester) =>
            ValidateRequest(requester) ? playerDamage : 0;

        public void SetPlayerDamage(GameObject requester, int value)
        {
            if (requester != null)
            {
                playerDamage = value;
            }
        }

        public PlayerMovementController GetPlayerMovementController(GameObject requester) =>
            ValidateRequest(requester) ? playerMovementController : null;

        public int GetPlayerHealth(GameObject requester) =>
            ValidateRequest(requester) ? playerHealth : 0;

        public void ReducePlayerHealth(GameObject requester, int value)
        {
            if (requester != null)
            {
                playerHealth = Mathf.Max(0, playerHealth - value);
            }
        }
        public void IncreasePlayerHealth(GameObject requester, int value)
        {
            if (requester != null)
            {
                playerHealth += value;
            }
        }

        public PlayerAttackController GetPlayerAttackController(GameObject requester) =>
            ValidateRequest(requester) ? playerAttackController : null;
    }
}
