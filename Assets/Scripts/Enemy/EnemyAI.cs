using UnityEngine;

namespace ShootEmUp 
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;

        private void FixedUpdate()
        {                   
            switch (_enemy.IsPointReached)
            {
                case true:
                    _enemy.Attack(Time.fixedDeltaTime);
                    break;
                case false:
                    _enemy.Move(Time.fixedDeltaTime);
                    break;
            }
        }
    }
}