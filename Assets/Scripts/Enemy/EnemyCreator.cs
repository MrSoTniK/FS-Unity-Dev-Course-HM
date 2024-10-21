using System;
using System.Collections;
using UnityEngine;

namespace ShootEmUp 
{
    public class EnemyCreator : MonoBehaviour
    {
        public Action<Enemy, bool> OnEnemyCreated;

        [SerializeField]
        private Enemy _prefab;

        [SerializeField]
        private PoolAbstract<GameObject> _enemiesPool;

        [SerializeField]
        private Transform _worldTransform;

        [SerializeField]
        private int _quantity;

        [SerializeField]
        private int _maxCount;

        private void Awake()
        {
            for (var i = 0; i < _quantity; i++)
            {
                Enemy enemy = Instantiate(_prefab, _enemiesPool.GetContainer());
                _enemiesPool.Enqueue(enemy.gameObject);
            }
        }

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(1, 2));

                if (!_enemiesPool.TryDequeue(out var enemyGO) || enemyGO.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy = Instantiate(_prefab, _enemiesPool.GetContainer());
                }         

                var isEnemyValidForFire = enemy != null && Condition(enemy.gameObject);
                OnEnemyCreated?.Invoke(enemy, isEnemyValidForFire);             
            }
        }

        private void FixedUpdate()
        {
            foreach (var enemyGO in GetEnemiesArray()) 
            {
                if (!enemyGO.TryGetComponent<Enemy>(out var enemy)) continue;
                if (!enemy.IsHealthZero) continue;

                enemyGO.transform.SetParent(_enemiesPool.GetContainer());

                _enemiesPool.RemoveFromActiveObjects(enemyGO);
                _enemiesPool.Enqueue(enemyGO);
            }            
        }

        public GameObject[] GetEnemiesArray() 
        {
            return _enemiesPool.GetArrayFromActiveObjects();
        }

        private bool Condition(GameObject enemyGO) 
        {
            return _enemiesPool.AddToActiveObjects(enemyGO) && 
                _enemiesPool.GetActiveObjectsCount() < _maxCount;             
        }
    }
}