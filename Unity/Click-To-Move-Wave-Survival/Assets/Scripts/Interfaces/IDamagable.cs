using UnityEngine;

namespace Interfaces
{
    public interface IDamagable 
    {
        float CurrentHealth { get; }
        float AttackSpeed { get; }
        float AttackDamage { get; }
        
        void TakeDamage(GameObject requester, float value);
        
    }
}
