using UnityEngine;

namespace ShootEmUp 
{
    public class EnemiesManager : MonoBehaviour
    {
        [SerializeField]
        private Spawner _enemiesSpawner;

        [SerializeField]
        private BulletManager _bulletManager;

        [SerializeField] 
        private Ship _target;

        [SerializeField] 
        private Transform _worldTransform;

        [SerializeField]
        private Transform[] _spawnPositions;

        [SerializeField]
        private Transform[] _attackPositions;   

        [SerializeField]
        private int _maxCount;

        private void FixedUpdate()
        {
            foreach (var enemyGO in _enemiesSpawner.GetArrayFromActiveObjects())
            {
                if (!enemyGO.TryGetComponent<Ship>(out var enemy)) continue;

                if (enemy.IsHealthZero)
                {
                    enemyGO.transform.SetParent(_enemiesSpawner.GetContainer());

                    _enemiesSpawner.RemoveFromActiveObjects(enemyGO);
                    _enemiesSpawner.Enqueue(enemyGO);
                }         
            }          
        }

        public void Spawn()
        {
            var enemy = _enemiesSpawner.SpawnComponent<Ship>();
            var isEnemyValidForFire = enemy != null && Condition(enemy.gameObject);

            InitEnemy(enemy, isEnemyValidForFire);
        }

        private bool Condition(GameObject enemyGO)
        {
            return _enemiesSpawner.AddToActiveObjects(enemyGO) &&
                _enemiesSpawner.GetActiveObjectsCount() < _maxCount;
        }

        public void InitEnemy(Ship enemy, bool isEnemyValidForFire) 
        {
            if (enemy == null || !enemy.TryGetComponent<EnemyAI>(out var enemyAI)) return;
     
            enemy.ResetHealth();
            enemy.transform.SetParent(_worldTransform);

            Transform spawnPosition = RandomPoint(_spawnPositions);
            enemy.transform.position = spawnPosition.position;  

            Transform attackPosition = RandomPoint(_attackPositions);
            enemyAI.Init(attackPosition.position, _target);          

            enemy.Weapon.SetBulletManager(_bulletManager, isEnemyValidForFire);
        }

        private Transform RandomPoint(Transform[] points)
        {
            int index = Random.Range(0, points.Length);
            return points[index];
        }
    }
}