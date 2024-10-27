using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Player _character;

        [SerializeField]
        private PlayerManager _playerManager;

        private void OnEnable()
        {
            _character.OnPlayerEnabled += _playerManager.SetManager;
            _character.OnHealthEmpty += _ => Time.timeScale = 0;
            _character.OnFireButtonClick += _playerManager.Shoot;
            _character.OnMoveDirectionChanged += _playerManager.Move;
        }
        private void OnDisable()
        {
            _character.OnPlayerEnabled -= _playerManager.SetManager;
            _character.OnHealthEmpty -= _ => Time.timeScale = 0;
            _character.OnFireButtonClick -= _playerManager.Shoot;
            _character.OnMoveDirectionChanged -= _playerManager.Move;
        }     
    }
}