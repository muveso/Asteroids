using System;
using Root.Settings;
using Root.Utils;
using UnityEngine;
using Zenject;
using AudioSettings = Root.Settings.AudioSettings;

namespace Root.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private float _acceleration;
        private IMemoryPool<AudioSource> _audioPool;
        private AudioSettings _audioSettings;
        private Camera _camera;
        private EngineSound _engineSound;
        private IInputSettings _inputSettings;

        private float _rotation;
        private ShipAccelerator _shipAccelerator;
        private ShipRotator _shipRotator;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            switch (_inputSettings.InputScheme)
            {
                case InputScheme.Keyboard:
                    _rotation = Input.GetAxisRaw("Horizontal") * _shipRotator.AngleVelocity;
                    _acceleration = Input.GetAxisRaw("Vertical");
                    break;
                case InputScheme.KeyboardMouse:
                    var direction = (_camera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                    _rotation = (Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg - transform.eulerAngles.z)
                        .FixAngle();
                    _acceleration = Input.GetAxisRaw("Vertical");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _shipRotator.Rotate(_rotation);
            _shipAccelerator.Accelerate(_acceleration);
            _engineSound.PlaySound(_acceleration);
        }

        [Inject]
        private void Construct
        (
            EngineSound engineSound,
            ShipAccelerator shipAccelerator,
            ShipRotator shipRotator,
            IInputSettings inputSettings
        )
        {
            _engineSound = engineSound;
            _shipRotator = shipRotator;
            _shipAccelerator = shipAccelerator;
            _inputSettings = inputSettings;
        }
    }
}