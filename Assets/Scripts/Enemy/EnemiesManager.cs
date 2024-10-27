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
        private Unit _target;

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
                if (!enemyGO.TryGetComponent<Enemy>(out var enemy)) continue;

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
            var enemy = _enemiesSpawner.SpawnComponent<Enemy>();
            var isEnemyValidForFire = enemy != null && Condition(enemy.gameObject);

            InitEnemy(enemy, isEnemyValidForFire);
        }

        private bool Condition(GameObject enemyGO)
        {
            return _enemiesSpawner.AddToActiveObjects(enemyGO) &&
                _enemiesSpawner.GetActiveObjectsCount() < _maxCount;
        }

        public void InitEnemy(Enemy enemy, bool isEnemyValidForFire) 
        {
            if (enemy == null) return;

            enemy.ResetHealth();
            enemy.transform.SetParent(_worldTransform);
            enemy.SetTarget(_target);

            Transform spawnPosition = RandomPoint(_spawnPositions);
            enemy.transform.position = spawnPosition.position;

            Transform attackPosition = RandomPoint(_attackPositions);
            enemy.SetDestination(attackPosition.position);

            enemy.Weapon?.SetBulletManager(_bulletManager, isEnemyValidForFire);
        }

        private Transform RandomPoint(Transform[] points)
        {
            int index = Random.Range(0, points.Length);
            return points[index];
        }
    }
}