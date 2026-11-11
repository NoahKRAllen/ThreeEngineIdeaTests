using UnityEngine;

namespace NPCControls
{
    public class UnitStats : MonoBehaviour
    {
        [SerializeField] private string unitName;
        [SerializeField] private int unitHealth;
        [SerializeField] private float unitAttackRange;
        [SerializeField] private float unitAttackSpeed;
        [SerializeField] private int unitDamage;
        [SerializeField] private float unitAggroRange;
        [SerializeField] private float unitMovementSpeed;
        [SerializeField] private bool isMobile;
        [SerializeField] private UnitMovementController unitMovementController;
        [SerializeField] private UnitTargetingController unitTargetingController;
        
        private bool ValidateRequest(GameObject requester)
        {
            //This will be used to handle the multiplayer logic in the future to ensure whatever is accessing code has permission to do so
            if (requester)
            {
                return true;
            }
            return false;
        }
        public string GetUnitName(GameObject requester) =>
            ValidateRequest(requester) ? unitName : null;
        public int GetUnitHealth(GameObject requester) =>
            ValidateRequest(requester) ? unitHealth : 0;
        public void ReduceUnitHealth(GameObject requester, int value)
        {
            if (requester != null)
            {
                unitHealth -= value;
            }
        }
        public void IncreaseUnitHealth(GameObject requester, int value)
        {
            if (requester != null)
            {
                unitHealth += value;
            }
        }

        public float GetUnitAttackRange(GameObject requester) =>
            ValidateRequest(requester) ? unitAttackRange : 0f;

        public float GetUnitAttackSpeed(GameObject requester) =>
            ValidateRequest(requester) ? unitAttackSpeed : 0f;
        public int GetUnitDamage(GameObject requester) =>
            ValidateRequest(requester) ? unitDamage : 0;

        public float GetUnitAggroRange(GameObject requester) =>
            ValidateRequest(requester) ? unitAggroRange : 0f;
        public float GetUnitMovementSpeed(GameObject requester) =>
            ValidateRequest(requester) ? unitMovementSpeed : 0f;
        public bool GetIsMobile(GameObject requester) =>
            ValidateRequest(requester) && isMobile;

        public UnitMovementController GetUnitMovementController(GameObject requester) =>
            ValidateRequest(requester) ? unitMovementController : null;
        public UnitTargetingController GetUnitTargetingController(GameObject requester) =>
            ValidateRequest(requester) ? unitTargetingController : null;
    }
}
