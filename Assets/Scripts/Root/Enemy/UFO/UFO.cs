using System;
using Root.BorderCrosser;
using UnityEngine;
using Zenject;

namespace Root.Enemy.UFO
{
    /// <summary>
    /// Represents a UFO enemy that moves horizontally and can damage other enemies on collision.
    /// </summary>
    public class UFO : MonoBehaviour, IEnemy
    {
        private IBorderCrosser _boundaries;
        private int _direction;
        private Transform _transform;

        private void Update()
        {
            transform.Translate(Vector2.right * _direction * 2 * Time.deltaTime);

            _transform.position = _boundaries.BoundariesCheck(_transform.position);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.TryGetComponent(out IEnemy enemy)) return;
            enemy.GetDamage();
            OnDamage.Invoke(gameObject);
        }

        /// <summary>
        /// Damages the UFO.
        /// </summary>
        public void GetDamage()
        {
            OnDamage.Invoke(gameObject);
        }

        /// <summary>
        /// Despawns the UFO.
        /// </summary>
        public void Despawn()
        {
            OnDamage.Invoke(gameObject);
        }

        private event Action<GameObject> OnDamage = _ => { };

        [Inject]
        private void Construct(IBorderCrosser borderCrosser)
        {
            _boundaries = borderCrosser;
        }

        /// <summary>
        /// Initializes the UFO with the specified direction, spawn position, and callback.
        /// </summary>
        /// <param name="direction">The direction to move in (-1 for left, 1 for right).</param>
        /// <param name="spawnPosition">The position to spawn the UFO at.</param>
        /// <param name="callback">The callback to invoke when the UFO is damaged or despawned.</param>
        public void Initialize
        (
            int direction,
            Vector2 spawnPosition,
            Action<GameObject> callback
        )
        {
            _transform = transform;
            _transform.position = spawnPosition;
            _direction = direction;
            OnDamage += callback;
        }
    }
}