using System.Collections;
using UnityEngine;

namespace ShootEmUp 
{
    public class EnemySpawnController : MonoBehaviour
    {
        [SerializeField] 
        private EnemiesManager _enemyManager;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(1, 2));
                _enemyManager.Spawn();
            }
        }
    }
}