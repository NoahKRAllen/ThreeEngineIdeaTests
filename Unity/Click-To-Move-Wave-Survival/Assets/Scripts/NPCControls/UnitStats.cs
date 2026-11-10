using UnityEngine;

namespace NPCControls
{
    public class UnitStats : MonoBehaviour
    {
        [SerializeField] private string unitName;
        [SerializeField] private float unitHealth;
        [SerializeField] private float unitAttackRange;
        [SerializeField] private float unitAttackSpeed;
        [SerializeField] private float unitDefense;
        [SerializeField] private float unitAggroRange;
        [SerializeField] private float unitMovementSpeed;
        [SerializeField] private bool isMobile;
        [SerializeField] private UnitMovementController unitMovementController;
        [SerializeField] private UnitTargetingController unitTargetingController;
        
        public string GetUnitName(GameObject requester)
                {
                    if (requester != null)
                    {
                        return unitName;
                    }
                    return null;
                }
        public float GetUnitHealth(GameObject requester)
                {
                    if (requester != null)
                    {
                        return unitHealth;
                    }
        
                    return 0;
                }
        public void ReduceUnitHealth(GameObject requester, float value)
        {
            if (requester != null)
            {
                unitHealth -= value;
            }
        }
        public void IncreaseUnitHealth(GameObject requester, float value)
        {
            if (requester != null)
            {
                unitHealth += value;
            }
        }
        public float GetUnitAttackRange(GameObject requester)
                {
                    if (requester != null)
                    {
                        return unitAttackRange;
                    }
                    return 0;
                }
        public float GetUnitAttackSpeed(GameObject requester)
                {
                    if (requester != null)
                    {
                        return unitAttackSpeed;
                    }
                    return 0;
                }
        public float GetUnitDefense(GameObject requester)
                {
                    if (requester != null)
                    {
                        return unitDefense;
                    }
                    return 0;
                }
        public float GetUnitAggroRange(GameObject requester)
                {
                    if (requester != null)
                    {
                        return unitAggroRange;
                    }
                    return 0;
                }
        public float GetUnitMovementSpeed(GameObject requester)
                {
                    if (requester != null)
                    {
                        return unitMovementSpeed;
                    }
                    return 0;
                }
        public bool GetIsMobile(GameObject requester)
                {
                    if (requester != null)
                    {
                        return isMobile;
                    }
        
                    return false;
                }
        public UnitMovementController GetUnitMovementController(GameObject requester)
                {
                    return requester != null ? unitMovementController : null;
                }
        public UnitTargetingController GetUnitTargetingController(GameObject requester)
        {
            if (requester != null)
            {
                return unitTargetingController;
            }

            return null;
        }
    }
}
