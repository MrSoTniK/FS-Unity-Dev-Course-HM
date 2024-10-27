using System;
using UnityEngine;

namespace ShootEmUp 
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private BulletManager _bulletManager;

        public void Shoot(Player player) 
        {
            player.Weapon.Shoot
                (
                    player.Weapon.FirePoint.transform.position,
                    player.Weapon.FirePoint.rotation * Vector3.up * 3
                );
        }

        public void Move(Player player, int direction) 
        {
            Vector2 moveDirection = new Vector2(direction, 0);
            Vector2 moveStep = moveDirection * Time.fixedDeltaTime * player.Speed;
            Vector2 targetPosition = player.Rigidbody.position + moveStep;

            player.Move(targetPosition);
        }

        public void SetManager(Player player)
        {
            player.Weapon.SetBulletManager(_bulletManager);
        }
    }
}