using System.Collections.Generic;
using NPCControls;
using PlayerControls;
using UnityEngine;

namespace SharedScripts
{
    public class CombatController : MonoBehaviour
    {
        private static readonly Dictionary<Transform, CombatController> Registry = new();
        private void OnEnable() => Registry[transform] = this;
        private void OnDisable() => Registry.Remove(transform);
        
        private bool _isDead = false;

        private bool _withinRange = false;
        private UnitStats _unitStats;
        private PlayerStats _playerStats;
        private float _nextAttackTime;

        [SerializeField] private CombatController target;

        private enum UnitType { Player, Npc, NotSetup }

        private UnitType _unitType = UnitType.NotSetup;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (TryGetComponent(out _unitStats))
            {
                _unitType = UnitType.Npc;
                _playerStats = null;
                
            }
            else if (TryGetComponent(out _playerStats))
            {
                _unitType = UnitType.Player;
                _unitStats = null;
            }
            else
            {
                Debug.LogError($"[CombatController] Missing required components on {gameObject.name}");
                gameObject.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            switch (_unitType)
            {
                case UnitType.Player:
                    PlayerUpdate();
                    break;
                case UnitType.Npc:
                    NpcUpdate();
                    break;
                case UnitType.NotSetup:
                    break;
                default:
                    Debug.LogError($"Somehow we are missing the UnitType variable completely in the CombatController on {gameObject.name}");
                    gameObject.SetActive(false);
                    break;
            }
        }

        private void PlayerUpdate()
        {
            if (_playerStats.GetPlayerHealth(gameObject) <= 0)
            {
                Die(gameObject);
            }

            if (_nextAttackTime > 0.0f)
            {
                _nextAttackTime -= Time.deltaTime;
            }

            if (!(_nextAttackTime <= 0.0f) || !_withinRange) return;
            _nextAttackTime = 1f / _playerStats.GetPlayerAttackRate(gameObject);
            if (!target) return;
            target.TakeDamage(gameObject, _playerStats.GetPlayerDamage(gameObject));
        }

        private void NpcUpdate()
        {
            if (_unitStats.GetUnitHealth(gameObject) <= 0)
            {
                Die(gameObject);
            }

            if (_nextAttackTime > 0.0f)
            {
                _nextAttackTime -= Time.deltaTime;
            }

            if (!(_nextAttackTime <= 0.0f) || !_withinRange) return;
            _nextAttackTime = 1f / _unitStats.GetUnitAttackSpeed(gameObject);
            if (!target) return;
            target.TakeDamage(gameObject, _unitStats.GetUnitDamage(gameObject));
        }

        private void TakeDamage(GameObject requester, int incomingDamage)
        {
            if (requester == null)
            {
                return;
            }
            switch (_unitType)
            {
                case UnitType.Player:
                    _playerStats.ReducePlayerHealth(gameObject, incomingDamage);
                    break;
                case UnitType.Npc:
                    _unitStats.ReduceUnitHealth(gameObject, incomingDamage);
                    break;
            }
        }
        
        private void Die(GameObject requester)
        {
            if (requester == null)
            {
                return;
            }
            //Setup this way so that multiple Die calls don't cause multiple attempts to destroy object, as well as any future events that happen on death happening only once
            if (_isDead)
            {
                return;
            }
            _isDead = true;
            Destroy(gameObject);
        }
        
        public void StartAttack(GameObject requester, Transform newTarget)
        {
            if (requester == null)
            {
                return;
            }
            if (!newTarget)
            {
                print($"{gameObject.name} tried to start attack on null target");
                return;
            }

            if (!Registry.TryGetValue(newTarget, out var targetController))
            {
                print($"No CombatController registered for {newTarget.name}");
                return;
            }

            _withinRange = true;
            target = targetController;
        }
        
        public void StopAttack(GameObject requester)
        {
            if (requester == null)
            {
                return;
            }
            _withinRange = false;
            target = null;
        }
    }
}
