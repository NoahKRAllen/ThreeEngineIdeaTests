using UnityEngine;
using UnityEngine.AI;

namespace GameControls
{
    public class NpcSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject unitPrefab;
        [SerializeField] private float spawnRadius;
        [SerializeField] private float spawnTimer;
        private float _currentTimer;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (unitPrefab == null)
            {
                print($"Missing Unit Prefab from {gameObject.name}");
                gameObject.SetActive(false);
            }
            _currentTimer = 0f;            
        }

        // Update is called once per frame
        void Update()
        {
            if (_currentTimer < spawnTimer)
            {
                _currentTimer += Time.deltaTime;
                return;
            }
            _currentTimer = 0f;
            SpawnAgentAtNearbyRandomPosition();
        }

        private void SpawnAgentAtNearbyRandomPosition()
        {
            Vector3 origin = transform.position;
            Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
            randomDirection += origin;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, spawnRadius, NavMesh.AllAreas))
            {
                GameObject unitObj = Instantiate(unitPrefab, hit.position, Quaternion.identity);
                NavMeshAgent agent =  unitObj.GetComponent<NavMeshAgent>();

                if (agent)
                {
                    agent.Warp(hit.position);
                    agent.enabled = true;
                }
            }
        }
    }
}
