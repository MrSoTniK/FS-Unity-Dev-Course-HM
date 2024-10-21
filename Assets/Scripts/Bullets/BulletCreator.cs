using UnityEngine;

namespace ShootEmUp
{
    public class BulletCreator : MonoBehaviour
    {           
        [SerializeField]
        private Bullet _prefab;

        [SerializeField]
        private int _quantity;

        [SerializeField]
        public Transform _worldTransform;

        [SerializeField]
        private PoolAbstract<GameObject> _bulletsPool;

        [SerializeField]
        private Cache _cache;

        private void Awake()
        {
            for (var i = 0; i < _quantity; i++)
            {
                Bullet bullet = Instantiate(_prefab, _bulletsPool.GetContainer());
                _bulletsPool.Enqueue(bullet.gameObject);
            }
        }

        private void OnEnable()
        {
            _cache.OnObjectRemoved += RemoveBullet;
        }

        private void OnDisable()
        {
            _cache.OnObjectRemoved -= RemoveBullet;
        }

        public void SpawnBullet(Vector2 position, Vector2 direction)
        {
            switch (_bulletsPool.TryDequeue(out var bulletGO))
            {
                case true:
                    bulletGO.transform.SetParent(_worldTransform);
                    break;
                case false:
                    bulletGO = Instantiate(_prefab, _worldTransform).gameObject;
                    break;
            }

            if (!_bulletsPool.AddToActiveObjects(bulletGO)) return;

            bulletGO.transform.position = position;

            if (!bulletGO.TryGetComponent<Bullet>(out var bullet)) return;

            bullet.OnBulletHit += RemoveBullet;

            bullet.SetVelocity(direction);
        }

        private void RemoveBullet(GameObject bulletGO)
        {
            if (!bulletGO.TryGetComponent<Bullet>(out var bullet) || !_bulletsPool.RemoveFromActiveObjects(bulletGO)) return;
            
            bullet.OnBulletHit -= RemoveBullet;
            bullet.transform.SetParent(_bulletsPool.GetContainer());
            _bulletsPool.Enqueue(bullet.gameObject);
        }
    }
}