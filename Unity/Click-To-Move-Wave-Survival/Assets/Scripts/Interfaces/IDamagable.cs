using UnityEngine;

namespace Interfaces
{
    public interface IDamagable 
    {
        float CurrentHealth { get; }
        void TakeDamage(GameObject requester, float value);
    }
}
