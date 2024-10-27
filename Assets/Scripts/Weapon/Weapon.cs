using UnityEngine;

namespace ShootEmUp 
{
    public class Weapon : MonoBehaviour
    {
        private BulletManager _bulletManager;
        private bool _canShoot;

        [SerializeField]
        private Transform _firePoint;

        public Transform FirePoint => _firePoint;

        public void SetBulletManager(BulletManager bulletManager, bool canShoot = true) 
        {
            _bulletManager = bulletManager;
            _canShoot = canShoot;
        }

        public void Shoot(Vector2 position, Vector2 direction) 
        {
            if (_bulletManager == null || !_canShoot) return;

            _bulletManager.SpawnBullet(position, direction);
        }
    }
}