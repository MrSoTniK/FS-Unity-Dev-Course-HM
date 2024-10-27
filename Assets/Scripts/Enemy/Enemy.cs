using UnityEngine;

namespace ShootEmUp
{
    public sealed class Enemy : Unit
    {
        public delegate void FireHandler(Vector2 position, Vector2 direction);        
        public event FireHandler OnFire;
     
        [SerializeField]
        private float _countdown;
  
        private Vector2 _destination;
        private float _currentTime;
        private bool _isPointReached;

        private Unit _target;

        public bool IsPointReached => _isPointReached;

        public void Reset()
        {
            _currentTime = _countdown;
        }

        public void SetTarget(Unit unit) 
        {
            _target = unit;
        }
        
        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isPointReached = false;
        }   

        public void Attack(float fixedDeltaTime) 
        {
            if (_target == null || _target.IsHealthZero) return;

            _currentTime -= fixedDeltaTime;
            if (_currentTime > 0) return;

            Vector2 startPosition = Weapon.FirePoint.position;
            Vector2 vector = (Vector2)_target.transform.position - startPosition;
            Vector2 direction = vector.normalized;

            Weapon.Shoot(startPosition, direction);

            _currentTime += _countdown;
        }

        public void Move(float fixedDeltaTime)
        {
            Vector2 vector = _destination - (Vector2)transform.position;
            if (vector.magnitude <= 0.25f)
            {
                _isPointReached = true;
                return;
            }

            Vector2 direction = vector.normalized * fixedDeltaTime;
            Vector2 nextPosition = Rigidbody.position + direction * Speed;
            base.Move(nextPosition);
        }      
    }
}