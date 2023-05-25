using Root.Settings;
using UnityEngine;
using Zenject;

namespace Root.Enemy.UFO
{
    /// <summary>
    /// Component that controls the shooting behavior of a UFO enemy.
    /// </summary>
    public class IUFOComponent : Weapon.Weapon
    {
        private float _delay;
        private float _maxDelay;
        private float _minDelay;
        private Transform _player;

        private float Delay => Random.Range(_minDelay, _maxDelay);

        public void Update()
        {
            _delay -= Time.deltaTime;

            if (!(_delay <= 0)) return;
            Fire();
            _delay = Delay;
        }

        [Inject]
        private void Construct(Transform player, GameSettings gameSettings)
        {
            _player = player;
            _minDelay = gameSettings.MinShootDelayUfo;
            _maxDelay = gameSettings.MaxShootDelayUfo;
        }

        /// <summary>
        /// Fires a projectile towards the player.
        /// </summary>
        public override void Fire()
        {
            var direction = (_player.position - transform.position).normalized;
            var rotation = Quaternion.Euler(new(0, 0, Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg));
            var position = transform.position;
            var projectile = ProjectilePool.Spawn();

            projectile.gameObject.SetActive(true);
            projectile.Initialize(4, Color.red, transform, position, rotation, Despawn);
        }
    }
}