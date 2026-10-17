using UnityEngine;

public class UnitStats : MonoBehaviour
{
    [SerializeField] private string unitName;
    public string GetUnitName(GameObject requester)
    {
        if (requester != null)
        {
            return unitName;
        }
        return null;
    }
    
    [SerializeField] private float unitHealth;
    public float GetUnitHealth(GameObject requester)
    {
        if (requester != null)
        {
            return unitHealth;
        }

        return 0;
    }
    public void SetUnitHealth(GameObject requester, float newHealth)
    {
        if (requester != null)
        {
            unitHealth = newHealth;
        }
    }
    
    [SerializeField] private float unitAttackRange;
    public float GetUnitAttackRange(GameObject requester)
    {
        if (requester != null)
        {
            return unitAttackRange;
        }
        return 0;
    }
    
    [SerializeField] private float unitAttackSpeed;
    public float GetUnitAttackSpeed(GameObject requester)
    {
        if (requester != null)
        {
            return unitAttackSpeed;
        }
        return 0;
    }
    
    [SerializeField] private float unitDefense;
    public float GetUnitDefense(GameObject requester)
    {
        if (requester != null)
        {
            return unitDefense;
        }
        return 0;
    }
    
    [SerializeField] private float unitAggroRange;
    public float GetUnitAggroRange(GameObject requester)
    {
        if (requester != null)
        {
            return unitAggroRange;
        }
        return 0;
    }
    
    [SerializeField] private float unitMovementSpeed;
    public float GetUnitMovementSpeed(GameObject requester)
    {
        if (requester != null)
        {
            return unitMovementSpeed;
        }
        return 0;
    }
    
}
