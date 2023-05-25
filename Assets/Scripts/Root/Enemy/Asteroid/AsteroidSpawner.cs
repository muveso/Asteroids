using System;
using Root.BorderCrosser;
using Root.Constants;
using Root.GameStarter;
using Root.Settings;
using UnityEngine;
using Zenject;
using AudioSettings = Root.Settings.AudioSettings;
using Random = UnityEngine.Random;

namespace Root.Enemy.Asteroid
{
    /// <summary>
    /// Spawns asteroids in the game.
    /// </summary>
    public class AsteroidSpawner : IAsteroidSpawner, IDisposable
    {
        private readonly IMemoryPool<Asteroid> _asteroidPool;
        private readonly AudioSource _audioSource;
        private readonly IBorderCrosser _borderCrosser;
        private readonly int _splitAngle;
        private readonly IStarter _starter;

        private int _count;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsteroidSpawner"/> class.
        /// </summary>
        /// <param name="asteroidPool">The asteroid pool.</param>
        /// <param name="audioPool">The audio pool.</param>
        /// <param name="audioSettings">The audio settings.</param>
        /// <param name="gameSettings">The game settings.</param>
        /// <param name="borderCrosser">The border crosser.</param>
        /// <param name="starter">The game starter.</param>
        public AsteroidSpawner
        (
            MemoryPool<Asteroid> asteroidPool,
            MemoryPool<AudioSource> audioPool,
            AudioSettings audioSettings,
            GameSettings gameSettings,
            IBorderCrosser borderCrosser,
            IStarter starter
        )
        {
            audioSettings.Initialize();
            _asteroidPool = asteroidPool;
            _audioSource = audioPool.Spawn();
            _audioSource.clip = audioSettings.AudioStorage[AudioConstants.Explosion];
            _splitAngle = gameSettings.AsteroidsSplitAngle;
            _count = gameSettings.AsteroidsStartCount;
            _borderCrosser = borderCrosser;
            _starter = starter;
            _starter.OnGameStart += Spawn;
        }

        /// <summary>
        /// Spawns asteroids.
        /// </summary>
        public void Spawn()
        {
            for (var i = 0; i < _count; i++)
            {
                var asteroid = GetAsteroid();
                asteroid.transform.eulerAngles = new(0, 0, Random.Range(-180, 180));
                asteroid.Initialize(3, _borderCrosser.Boundaries[Random.Range(0, 3)], Despawn, SpawnAfterDamage);
            }
        }

        /// <summary>
        /// Disposes the asteroid spawner.
        /// </summary>
        public void Dispose()
        {
            _starter.OnGameStart -= Spawn;
        }

        private Asteroid GetAsteroid()
        {
            var asteroid = _asteroidPool.Spawn();
            asteroid.gameObject.SetActive(true);
            return asteroid;
        }

        private void SpawnAfterDamage(Transform prevAsteroid, int health)
        {
            _audioSource.Play();
            var asteroid = GetAsteroid();
            var transform = asteroid.transform;
            var asteroidEuler = prevAsteroid.eulerAngles;
            transform.eulerAngles = new(0, 0, asteroidEuler.z - _splitAngle);
            prevAsteroid.eulerAngles = new(0, 0, asteroidEuler.z + _splitAngle);
            asteroid.Initialize(health, prevAsteroid.position, Despawn, SpawnAfterDamage);
        }

        private void Despawn(Asteroid asteroid)
        {
            asteroid.gameObject.SetActive(false);
            _asteroidPool.Despawn(asteroid);
            _audioSource.Play();

            if (_asteroidPool.NumActive != 0) return;
            _count += 1;
            Spawn();
        }
    }
}