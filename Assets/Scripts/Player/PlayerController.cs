using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Ship _player;
           
        private void Update()
        {
            if (_player.IsHealthZero) return;

            if (Input.GetKeyDown(KeyCode.Space))
                _player.Weapon.Shoot
                (
                    _player.Weapon.FirePoint.transform.position,
                    _player.Weapon.FirePoint.rotation * Vector3.up * 3
                );

            if (Input.GetKey(KeyCode.LeftArrow))
                _player.Move(-1);
            else if (Input.GetKey(KeyCode.RightArrow))
                _player.Move(1);
            else
                _player.Move(0);
        }
    }
}