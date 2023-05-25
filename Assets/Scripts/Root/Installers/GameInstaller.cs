using Root.Player;
using Root.Settings;
using UnityEngine;
using Zenject;

namespace Root.Installers
{
    /// <summary>
    /// Installs bindings for the game objects and settings.
    /// </summary>
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameSettings settings;

        public override void InstallBindings()
        {
            Container
                .BindInstance(settings);

            Container
                .Bind<ShipAccelerator>()
                .AsSingle();

            Container
                .Bind<ShipRotator>()
                .AsSingle();

            Container
                .Bind<EngineSound>()
                .AsSingle();
        }
    }
}