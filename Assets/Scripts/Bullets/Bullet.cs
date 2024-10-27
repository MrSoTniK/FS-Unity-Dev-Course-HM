using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public Action<GameObject> OnBulletHit;

        [SerializeField]
        private int _damage;   

        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            DealDamage(collision);
        }

        public void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.velocity = velocity;
        }

        private void DealDamage(Collision2D collision) 
        {
            if (_damage <= 0)
                return;

            if (collision.gameObject.TryGetComponent(out Ship unit))
            {
                unit.ReceiveDamage(_damage);
                OnBulletHit?.Invoke(gameObject);
            }
        }
    }
}