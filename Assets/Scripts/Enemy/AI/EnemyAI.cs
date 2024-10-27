using UnityEngine;

namespace ShootEmUp 
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        private Ship _enemy;

        [SerializeField]
        private MoveAgent _moveAgent;

        [SerializeField]
        private AttakAgent _attakAgent;

        private Vector2 _destination;
        private Ship _target;

        private void FixedUpdate()
        {
            switch (Condition(out var vector)) 
            {
                case true:
                    _attakAgent.Attack(_enemy, Time.fixedDeltaTime, _target);
                    break;
                case false:
                    _moveAgent.Move(_enemy, Time.fixedDeltaTime, ref _destination);
                    break;
            }
        }


        public void Init(Vector2 endPoint, Ship unit)
        {
            _destination = endPoint;
            _target = unit;
        }

        private bool Condition(out Vector2 vector)
        {
            vector = _destination - (Vector2)transform.position;
            return vector.magnitude <= 0.25f;
        }
    }
}