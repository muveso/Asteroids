using System;
using Root.BorderCrosser;
using UnityEngine;
using Zenject;

namespace Root.Player
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;
        private IBorderCrosser _borderCrosser;
        private float _distance;

        private bool _isHit;
        private Action<Projectile> _onCollision;
        private Transform _owner;
        private float _passedWay;
        private Vector3 _position;
        private Quaternion _rotation;
        private float _speed;

        private void Update()
        {
            transform.Translate(Vector2.up * (_speed * Time.deltaTime));

            _passedWay += _speed * Time.deltaTime;

            if (_passedWay > _distance)
            {
                _onCollision.Invoke(this);
                return;
            }

            CheckBoundries();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.transform == _owner) return;
            if (!col.TryGetComponent(out IDamageable damagable) || _isHit) return;
            _isHit = true;
            damagable.GetDamage();
            _onCollision.Invoke(this);
        }

        [Inject]
        private void Construct(IBorderCrosser borderCrosser)
        {
            _borderCrosser = borderCrosser;
            _distance = Vector2.Distance(borderCrosser.Boundaries[0], borderCrosser.Boundaries[2]);
        }

        public void Initialize
        (
            float speed,
            Color color,
            Transform owner,
            Vector3 position,
            Quaternion rotation,
            Action<Projectile> action
        )
        {
            _passedWay = 0;
            sprite.color = color;
            _isHit = false;
            _speed = speed;
            _owner = owner;
            transform.position = position;
            transform.rotation = rotation;
            _onCollision ??= action;
        }

        private void CheckBoundries()
        {
            transform.position = _borderCrosser.BoundariesCheck(transform.position);
        }
    }
}