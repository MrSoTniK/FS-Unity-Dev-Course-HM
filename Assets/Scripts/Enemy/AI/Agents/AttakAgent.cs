using UnityEngine;

namespace ShootEmUp 
{
    public class AttakAgent : MonoBehaviour
    {
        [SerializeField]
        private float _countdown;

        private float _currentTime;

        private void Reset()
        {
            _currentTime = _countdown;
        }
    
        public void Attack(Ship ship, float fixedDeltaTime, Ship target)
        {            
            if (target == null || target.IsHealthZero) return;

            _currentTime -= fixedDeltaTime;
            if (_currentTime > 0) return;

            Vector2 startPosition = ship.Weapon.FirePoint.position;
            Vector2 vector = (Vector2)target.transform.position - startPosition;
            Vector2 direction = vector.normalized;

            ship.Weapon.Shoot(startPosition, direction);

            _currentTime += _countdown;
        }
    }
}