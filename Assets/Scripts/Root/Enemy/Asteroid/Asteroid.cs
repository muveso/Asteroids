using System;
using Root.BorderCrosser;
using Root.UI;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Root.Enemy.Asteroid
{
    /// <summary>
    /// Represents an asteroid enemy that can take damage and be destroyed.
    /// </summary>
    public class Asteroid : MonoBehaviour, IEnemy
    {
        private IBorderCrosser _borderCrosser;
        private Transform _childSprite;
        private int _health = 3;
        private Action<Transform, int> _onDamage;
        private Action<Asteroid> _onDestroy;
        private IScoreSystem _scoreSystem;
        private float _speed = 2;
        private Transform _transform;
        
        // create public property for health, speed, and score
        public int Health => _health;
        public float Speed => _speed;

        // create public property for score system
        public IScoreSystem ScoreSystem
        {
            get => _scoreSystem;
            set => _scoreSystem = value;
        }
        
        // create public property for border crosser
        public IBorderCrosser BorderCrosser
        {
            get => _borderCrosser;
            set => _borderCrosser = value;
        }

        public void Start()
        {
            _transform = transform;
        }

        public void Update()
        {
            _transform.Translate(Vector2.up * (_speed * Time.deltaTime));

            _transform.position = _borderCrosser.BoundariesCheck(_transform.position);
        }

        /// <summary>
        /// Inflicts damage to the asteroid and updates its health and appearance.
        /// </summary>
        public void GetDamage()
        {
            AddScore(_health);
            _health -= 1;
            if (_health > 0)
            {
                _onDamage.Invoke(_transform, _health);
                UpdateAsteroid(_health);
            }
            else
            {
                _onDestroy.Invoke(this);
            }
        }

        /// <summary>
        /// Destroys the asteroid.
        /// </summary>
        public void Despawn()
        {
            _onDestroy.Invoke(this);
        }

        [Inject]
        private void Construct(IScoreSystem scoreSystem, IBorderCrosser borderCrosser)
        {
            _scoreSystem = scoreSystem;
            _borderCrosser = borderCrosser;
        }

        /// <summary>
        /// Initializes the asteroid with the specified health, position, and callbacks.
        /// </summary>
        /// <param name="health">The initial health of the asteroid.</param>
        /// <param name="position">The initial position of the asteroid.</param>
        /// <param name="despawnAction">The callback to invoke when the asteroid is destroyed.</param>
        /// <param name="spawnAction">The callback to invoke when the asteroid takes damage.</param>
        public void Initialize
        (
            int health,
            Vector3 position,
            Action<Asteroid> despawnAction,
            Action<Transform, int> spawnAction
        )
        {
            _transform = transform;
            _transform.position = position;
            _onDestroy ??= despawnAction;
            _onDamage ??= spawnAction;
            _health = health;

            UpdateAsteroid(_health);
        }

        private void UpdateAsteroid(int health)
        {
            _transform.localScale = Vector2.one * (health * .5f);
            _transform.GetChild(0).eulerAngles = new(0, 0, Random.Range(-180, 180));
            _speed = (5 - health) * 0.5f;
        }

        private void AddScore(int value)
        {
            var type = value switch
            {
                1 => EnemyType.SmallAsteroid,
                2 => EnemyType.MediumAsteroid,
                3 => EnemyType.LargeAsteroid,
                _ => throw new ArgumentOutOfRangeException()
            };
            _scoreSystem.AddScore(type);
        }
    }
}