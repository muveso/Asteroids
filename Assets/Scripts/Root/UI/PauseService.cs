using System;
using Root.Player;
using UnityEngine;

namespace Root.UI
{
    public class PauseService : IDisposable
    {
        private readonly PauseInput _pauseInput;

        private readonly PlayerMovement _playerMovement;
        private readonly ShipWeapon _playerWeapon;

        public PauseService(PauseInput pauseInput, Transform player)
        {
            _pauseInput = pauseInput;
            _playerMovement = player.GetComponent<PlayerMovement>();
            _playerWeapon = player.GetComponent<ShipWeapon>();

            _pauseInput.OnPause += ChangeState;
        }

        public void Dispose()
        {
            _pauseInput.OnPause -= ChangeState;
        }

        private void ChangeState(bool value)
        {
            _playerMovement.enabled = value;
            _playerWeapon.enabled = value;
        }
    }
}