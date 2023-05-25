using NSubstitute;
using NUnit.Framework;
using Root.BorderCrosser;
using Root.Enemy;
using Root.Enemy.Asteroid;
using UnityEngine;
using Root.UI;

namespace Tests
{
    public class AsteroidTests
    {
        private Asteroid _asteroid;
        private IScoreSystem _scoreSystemSubstitute;
        private IBorderCrosser _borderCrosserSubstitute;

        [SetUp]
        public void Setup()
        {
            // Create a new Asteroid instance for each test
            _asteroid = new GameObject().AddComponent<Asteroid>();

            // Create a substitute implementation of the IScoreSystem interface
            _scoreSystemSubstitute = Substitute.For<IScoreSystem>();
            _borderCrosserSubstitute = Substitute.For<IBorderCrosser>();
            
            // Set the substitute implementation as the ScoreSystem dependency
            _asteroid.ScoreSystem = _scoreSystemSubstitute;
            _asteroid.BorderCrosser = _borderCrosserSubstitute;
        }

        [TearDown]
        public void Teardown()
        {
            // Destroy the Asteroid instance after each test
            Object.Destroy(_asteroid.gameObject);
        }

        [Test]
        public void Asteroid_Start_SetsTransform()
        {
            // Arrange
            var expectedTransform = _asteroid.transform;

            // Act
            _asteroid.Start();

            // Assert
            Assert.AreEqual(expectedTransform, _asteroid.transform);
        }

        [Test]
        public void Asteroid_Update_TranslatesTransform()
        {
            // Arrange
            var expectedPosition = _asteroid.transform.position + Vector3.up * (_asteroid.Speed * Time.deltaTime);

            // Act
            _asteroid.Update();

            // Assert
            Assert.AreEqual(expectedPosition, _asteroid.transform.position);
        }

        [Test]
        public void Asteroid_Update_BoundariesCheck()
        {
            // Arrange
            var expectedPosition = new Vector3(0, 0, 0);
            _asteroid.transform.position = new Vector3(0, -10, 0);

            // Act
            _asteroid.Update();

            // Assert
            Assert.AreEqual(expectedPosition, _asteroid.transform.position);
        }

        [Test]
        public void Asteroid_GetDamage_AddsScore()
        {
            // Arrange
            var expectedScore = (EnemyType) _asteroid.Health;

            // Act
            _asteroid.GetDamage();

            // Assert
            _scoreSystemSubstitute.Received(1).AddScore(expectedScore);
        }

        [Test]
        public void Asteroid_GetDamage_DecrementsHealth()
        {
            // Arrange
            var expectedHealth = _asteroid.Health - 1;

            // Act
            _asteroid.GetDamage();

            // Assert
            Assert.AreEqual(expectedHealth, _asteroid.Health);
        }
    }
}