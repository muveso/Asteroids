using System;
using System.Linq;
using Root.Constants;
using Root.Settings;
using UnityEngine;
using Zenject;

namespace Root.Player
{
    public class ShipWeapon : Weapon.Weapon
    {
        private float[] _bulletCount;
        private bool _fire;
        private IInputSettings _inputSettings;

        private void Update()
        {
            for (var i = 0; i < _bulletCount.Length; i++)
                if (_bulletCount[i] > 0)
                    _bulletCount[i] -= Time.deltaTime;

            if (!_bulletCount.Any(x => x <= 0)) return;
            switch (_inputSettings.InputScheme)
            {
                case InputScheme.Keyboard:
                    if (Input.GetKeyDown(KeyCode.Space)) Fire();

                    break;
                case InputScheme.KeyboardMouse:
                    if (Input.GetMouseButtonDown(0)) Fire();

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [Inject]
        private void Construct(IInputSettings inputSettings, GameSettings gameSettings)
        {
            _inputSettings = inputSettings;
            _bulletCount = new float[gameSettings.BulletsPerSecond];
        }

        public override void Fire()
        {
            ResetTimer();

            var rot = Quaternion.Euler(new(0, 0, transform.eulerAngles.z));
            var position = transform.position + transform.up / 2;
            var projectile = ProjectilePool.Spawn();
            projectile.gameObject.SetActive(true);
            projectile.Initialize(4, Color.green, transform, position, rot, Despawn);

            var audio = AudioPool.Spawn();
            audio.PlayOneShot(AudioSettings.AudioStorage[AudioConstants.Fire]);
            AudioPool.Despawn(audio);
        }


        private void ResetTimer()
        {
            for (var i = 0; i < _bulletCount.Length; i++)
            {
                if (!(_bulletCount[i] <= 0)) continue;
                _bulletCount[i] = 1;
                break;
            }
        }
    }
}