using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Player : Unit
    {
        public Action<Player, int> OnHealthChanged;
        public Action<Player> OnHealthEmpty;

        public Action<Player> OnFireButtonClick;
        public Action<Player, int> OnMoveDirectionChanged;
        public Action<Player> OnPlayerEnabled;

        private int _moveDirection;

        public int MoveDirection => _moveDirection;

        private void OnEnable()
        {
            ResetHealth();
            OnPlayerEnabled?.Invoke(this);
        }

        public override void ReceiveDamage(int damage)
        {
            base.ReceiveDamage(damage);
            OnHealthChanged?.Invoke(this, CurrentHealth);

            if (IsHealthZero) 
                OnHealthEmpty?.Invoke(this);
        }

        private void Update()
        {
            if (IsHealthZero) return;

            if (Input.GetKeyDown(KeyCode.Space))
                OnFireButtonClick?.Invoke(this);

            if (Input.GetKey(KeyCode.LeftArrow))
                OnMoveDirectionChanged?.Invoke(this, -1);
            else if (Input.GetKey(KeyCode.RightArrow))
                OnMoveDirectionChanged?.Invoke(this, 1);
            else
                OnMoveDirectionChanged?.Invoke(this, 0);
        }
    }
}