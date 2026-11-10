using Interfaces;
using UnityEngine;

namespace PlayerControls
{
    public class PlayerStats : MonoBehaviour, IDamagable
    {
        public float CurrentHealth => playerHealth;

        public void TakeDamage(GameObject requester, float value)
        {
            if (requester != null)
            {
                playerHealth = Mathf.Max(0, playerHealth - value);
            }
        }
        [SerializeField] float playerMovementSpeed;

        [Tooltip("Attacks per second (1 = 1APS, 1.5 = 1.5APS")] [SerializeField]
        float playerAttackRate;

        [SerializeField] float playerAttackRange;

        [SerializeField] float playerAggroRange;

        [SerializeField] float playerDamage;


        [SerializeField] float playerHealth;


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
            ValidateRequest(requester) ? playerAttackRate : 0f;

        public float GetPlayerAttackRange(GameObject requester) =>
            ValidateRequest(requester) ? playerAttackRange : 0f;

        public float GetPlayerAggroRange(GameObject requester) =>
            ValidateRequest(requester) ? playerAggroRange : 0f;

        public float GetPlayerDamage(GameObject requester) =>
            ValidateRequest(requester) ? playerDamage : 0f;

        public void SetPlayerDamage(GameObject requester, float value)
        {
            if (requester != null)
            {
                playerDamage = value;
            }
        }

        public PlayerMovementController GetPlayerMovementController(GameObject requester) =>
            ValidateRequest(requester) ? playerMovementController : null;

        public float GetPlayerHealth(GameObject requester) =>
            ValidateRequest(requester) ? playerHealth : 0f;
        

        private void IncreasePlayerHealth(GameObject requester, int value)
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
