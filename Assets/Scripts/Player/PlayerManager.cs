using UnityEngine;

namespace ShootEmUp 
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private BulletCreator _bulletCreator;

        public void Shoot(Player player, Vector2 distance) 
        {
            _bulletCreator.SpawnBullet(player.FirePoint.position, distance);
        }

        public void Move(Player player, int direction) 
        {
            Vector2 moveDirection = new Vector2(direction, 0);
            Vector2 moveStep = moveDirection * Time.fixedDeltaTime * player.Speed;
            Vector2 targetPosition = player.Rigidbody.position + moveStep;

            player.Move(targetPosition);
        }
    }
}