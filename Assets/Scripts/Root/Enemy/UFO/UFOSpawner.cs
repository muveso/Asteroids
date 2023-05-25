using Root.BorderCrosser;
using Root.Constants;
using Root.Settings;
using Root.UI;
using UnityEngine;
using Zenject;
using AudioSettings = Root.Settings.AudioSettings;

namespace Root.Enemy.UFO
{
    /// <summary>
    /// Spawns UFO enemies at random intervals and positions.
    /// </summary>
    public class UFOSpawner : MonoBehaviour
    {
        [SerializeField] private UFO ufo;
        private AudioSettings _audioSettings;
        private AudioSource _audioSource;
        private IBorderCrosser _borderCrosser;
        private float _delay;
        private DiContainer _diContainer;

        private bool _isSpawned;
        private float _maxTimeSpawn;
        private float _maxY;
        private float _minTimeSpawn;
        private float _minY;
        private float _offset;
        private IScoreSystem _scoreSystem;

        private float Delay => Random.Range(_minTimeSpawn, _maxTimeSpawn);

        private void Update()
        {
            if (_isSpawned) return;
            _delay -= Time.deltaTime;
            if (_delay <= 0) Spawn();
        }

        [Inject]
        private void Construct
        (
            DiContainer diContainer,
            IBorderCrosser borderCrosser,
            IScoreSystem scoreSystem,
            MemoryPool<AudioSource> audioPool,
            GameSettings gameSettings,
            AudioSettings audioSettings
        )
        {
            _diContainer = diContainer;
            _scoreSystem = scoreSystem;
            _borderCrosser = borderCrosser;

            _audioSettings = audioSettings;
            _audioSettings.Initialize();
            _audioSource = audioPool.Spawn();
            _audioSource.clip = audioSettings.AudioStorage[AudioConstants.Explosion];

            _maxY = borderCrosser.Boundaries[1].y;
            _minY = borderCrosser.Boundaries[0].y;
            _offset = (_maxY - _minY) * 0.2f;
            _maxY -= _offset;
            _minY += _offset;

            _minTimeSpawn = gameSettings.MinTimeSpawnUfo;
            _maxTimeSpawn = gameSettings.MaxTimeSpawnUfo;
        }

        private void ObjectDestroy(GameObject instance)
        {
            Destroy(instance);
            _audioSource.Play();

            _scoreSystem.AddScore(EnemyType.Ufo);
            _isSpawned = false;
            _delay = Delay;
        }

        /// <summary>
        /// Spawns a UFO enemy at a random position.
        /// </summary>
        private void Spawn()
        {
            var instance = _diContainer.InstantiatePrefab(ufo).GetComponent<UFO>();
            var position = new Vector2(_borderCrosser.Boundaries[Random.Range(0, 4)].x, Random.Range(_minY, _maxY));
            instance.Initialize(Random.Range(0, 2) * 2 - 1, position, ObjectDestroy);

            _isSpawned = true;
        }
    }
}