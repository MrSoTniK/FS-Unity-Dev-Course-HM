using UnityEngine;

namespace ShootEmUp 
{
    public class Unit : MonoBehaviour
    {
        [SerializeField]
        private Transform _firePoint;

        [SerializeField]
        private int _maxHealth;

        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private float _speed = 5.0f;

        private int _currentHealth;

        public int CurrentHealth => _currentHealth;
        public bool IsHealthZero => _currentHealth <= 0;

        public Transform FirePoint => _firePoint;
        public float Speed => _speed;

        public Rigidbody2D Rigidbody => _rigidbody;

        public virtual void Move(Vector3 targetPosition)
        {
            _rigidbody.MovePosition(targetPosition);
        }

        public virtual void ReceiveDamage(int damage)
        {
            _currentHealth = Mathf.Max(0, _currentHealth - damage);
        }

        public virtual void ResetHealth()
        {
            _currentHealth = _maxHealth;
        }
    }
}