using UnityEngine;

namespace ShootEmUp 
{
    public class EnemiesManager : MonoBehaviour
    {
        [SerializeField]
        private EnemyCreator _enemyCreator;

        [SerializeField]
        private BulletCreator _bulletCreator;

        [SerializeField] 
        private Unit _target;

        [SerializeField] 
        private Transform _worldTransform;

        [SerializeField]
        private Transform[] _spawnPositions;

        [SerializeField]
        private Transform[] _attackPositions;   

        private void FixedUpdate()
        {
            foreach (var enemyGO in _enemyCreator.GetEnemiesArray())
            {
                if (!enemyGO.TryGetComponent<Enemy>(out var enemy)) continue;

                if (enemy.IsHealthZero)
                {
                    enemy.OnFire -= OnFire;
                    continue;
                }

                switch (enemy.IsPointReached) 
                {
                    case true:
                        enemy.Attack(_target, Time.fixedDeltaTime);
                        break;
                    case false:
                        enemy.Move(_target.transform.position, Time.fixedDeltaTime);
                        break;
                }             
            }          
        }

        public void InitEnemy(Enemy enemy, bool isEnemyValidForFire) 
        {
            if (enemy == null) return;

            enemy.ResetHealth();
            enemy.transform.SetParent(_worldTransform);

            Transform spawnPosition = RandomPoint(_spawnPositions);
            enemy.transform.position = spawnPosition.position;

            Transform attackPosition = RandomPoint(_attackPositions);
            enemy.SetDestination(attackPosition.position);

            if (isEnemyValidForFire)
                enemy.OnFire += OnFire;
        }

        private void OnFire(Vector2 position, Vector2 direction) 
        {
            _bulletCreator.SpawnBullet(position, direction * 2);       
        }

        private Transform RandomPoint(Transform[] points)
        {
            int index = Random.Range(0, points.Length);
            return points[index];
        }
    }
}