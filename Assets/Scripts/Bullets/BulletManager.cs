using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletManager : MonoBehaviour
    {                  
        [SerializeField]
        private Spawner _spawner;

        [SerializeField]
        private LevelBounds _levelBounds;

        private readonly List<GameObject> m_cache = new();    

        private void FixedUpdate()
        {
            ResetCache();
        }

        public void SpawnBullet(Vector2 position, Vector2 direction)
        {
            var bulletGO = _spawner.SpawnGameObject();
            if (!bulletGO.TryGetComponent<Bullet>(out var bullet)) return;

            if (!_spawner.AddToActiveObjects(gameObject)) return;
            gameObject.transform.position = position;

            bullet.OnBulletHit += RemoveBullet;

            bullet.SetVelocity(direction);
        }     

        private void RemoveBullet(GameObject bulletGO)
        {
            if (!bulletGO.TryGetComponent<Bullet>(out var bullet) || !_spawner.RemoveFromActiveObjects(bulletGO)) return;
            
            bullet.OnBulletHit -= RemoveBullet;
            bullet.transform.SetParent(_spawner.GetContainer());
            _spawner.Enqueue(bullet.gameObject);
        }

        private void ResetCache()
        {
            m_cache.Clear();
            var array = _spawner.GetArrayFromActiveObjects();
            m_cache.AddRange(array);

            for (int i = 0, count = m_cache.Count; i < count; i++)
            {
                GameObject go = m_cache[i];
                if (!_levelBounds.InBounds(go.transform.position))
                {
                    RemoveBullet(go);
                }
            }
        }
    }
}