using Root.Enemy.Asteroid;
using Root.Enemy.UFO;
using Root.GameStarter;
using Root.Player;
using Root.Settings;
using Root.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using AudioSettings = Root.Settings.AudioSettings;

namespace Root.Installers
{
    /// <summary>
    /// Installs bindings for the game objects and settings in the scene.
    /// </summary>
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform player;
        [SerializeField] private HealthPanel healthPanel;
        [SerializeField] private GameObject monoUfoWeapon;
        [SerializeField] private PauseInput pauseInput;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Asteroid asteroidPrefab;
        [SerializeField] private StarterInput starterInput;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private AudioSettings audioSettings;
        [SerializeField] private StartSettings startSettings;

        [FormerlySerializedAs("boundariesSettings")] [SerializeField]
        private BoundarySettings boundarySettings;


        public override void InstallBindings()
        {
            Container
                .BindInstance(audioSettings)
                .AsSingle();

            Container
                .BindInstance(pauseInput)
                .AsSingle();

            Container
                .BindInstance(player)
                .AsSingle();

            Container
                .BindInterfacesTo<BorderCrosser.BorderCrosser>()
                .AsSingle()
                .WithArguments(boundarySettings)
                .NonLazy();

            Container
                .BindInterfacesTo<ScoreSystem>()
                .AsSingle();

            Container
                .BindInterfacesTo<InputSettings>()
                .AsSingle();

            Container
                .Bind<ScoreView>()
                .AsSingle()
                .WithArguments(scoreText)
                .NonLazy();


            Container
                .Bind<IUFOComponent>()
                .FromComponentOn(monoUfoWeapon)
                .AsSingle();


            Container
                .BindInterfacesTo<StarterInput>()
                .FromComponentInHierarchy(starterInput)
                .AsSingle();

            Container
                .BindInterfacesTo<StartSettings>()
                .FromInstance(startSettings)
                .AsSingle();

            Container
                .Bind<StartPanel>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<AsteroidSpawner>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<PauseService>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<HealthPanel>()
                .FromInstance(healthPanel)
                .AsSingle()
                .NonLazy();

            Container
                .BindMemoryPool<AudioSource, MemoryPool<AudioSource>>()
                .WithInitialSize(4)
                .FromComponentInNewPrefab(audioSettings.AudioSourcePrefab)
                .UnderTransformGroup("Audio Memory Pool");

            Container
                .BindMemoryPool<Projectile, MemoryPool<Projectile>>()
                .WithInitialSize(3)
                .FromComponentInNewPrefab(projectilePrefab)
                .UnderTransformGroup("Projectile Memory Pool");

            Container
                .BindMemoryPool<Asteroid, MemoryPool<Asteroid>>()
                .WithInitialSize(6)
                .FromComponentInNewPrefab(asteroidPrefab)
                .UnderTransformGroup("Asteroids Memory Pool");
        }
    }
}