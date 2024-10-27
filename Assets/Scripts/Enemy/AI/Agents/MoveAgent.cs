using UnityEngine;

namespace ShootEmUp 
{
    public class MoveAgent : MonoBehaviour
    {
        public void Move(Ship ship, float fixedDeltaTime, ref Vector2 destination)
        {
            var vector = destination - (Vector2)transform.position;
            Vector2 direction = vector.normalized * fixedDeltaTime;
            Vector2 nextPosition = ship.Rigidbody.position + direction * ship.Speed;
            ship.Move(nextPosition);
        }
    }
}