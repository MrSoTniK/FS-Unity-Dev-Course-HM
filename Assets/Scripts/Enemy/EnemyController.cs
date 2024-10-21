using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp 
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private EnemyCreator _enemyCreator;

        [SerializeField]
        private EnemiesManager _enemiesManager;

        private void OnEnable()
        {
            _enemyCreator.OnEnemyCreated += _enemiesManager.InitEnemy;
        }

        private void OnDisable()
        {
            _enemyCreator.OnEnemyCreated -= _enemiesManager.InitEnemy;
        }
    }
}