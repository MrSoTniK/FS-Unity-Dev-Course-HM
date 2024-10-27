using UnityEngine;

namespace ShootEmUp 
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField]
        private Ship _player;

        [SerializeField] 
        private BulletManager _bulletManager;

        private void Awake()
        {
            _player.Weapon.SetBulletManager(_bulletManager);
        }
    }
}