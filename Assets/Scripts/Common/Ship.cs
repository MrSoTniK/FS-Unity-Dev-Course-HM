using UnityEngine;

namespace ShootEmUp 
{
    public class Ship : MonoBehaviour
    {
        [SerializeField]
        private bool _isPlayer;

        [SerializeField]
        private int _maxHealth;

        [SerializeField]
        private Weapon _weapon;

        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private float _speed = 5.0f;

        private int _currentHealth;

        public int CurrentHealth => _currentHealth;
        public bool IsHealthZero => _currentHealth <= 0;

        public float Speed => _speed;

        public Rigidbody2D Rigidbody => _rigidbody;

        public Weapon Weapon => _weapon;

        private void OnEnable()
        {
            ResetHealth();
        }

        public virtual void Move(Vector3 targetPosition)
        {
            _rigidbody.MovePosition(targetPosition);
        }

        public virtual void ReceiveDamage(int damage)
        {
            _currentHealth = Mathf.Max(0, _currentHealth - damage);

            if (_isPlayer && IsHealthZero)
                Time.timeScale = 0;
        }

        public virtual void ResetHealth()
        {
            _currentHealth = _maxHealth;
        }

        public void Move(int direction)
        {
            Vector2 moveDirection = new Vector2(direction, 0);
            Vector2 moveStep = moveDirection * Time.fixedDeltaTime * Speed;
            Vector2 targetPosition = Rigidbody.position + moveStep;

            Move(targetPosition);
        }
    }
}