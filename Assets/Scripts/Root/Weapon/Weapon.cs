using Root.BorderCrosser;
using Root.Player;
using UnityEngine;
using Zenject;
using AudioSettings = Root.Settings.AudioSettings;

namespace Root.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        protected IMemoryPool<AudioSource> AudioPool;
        protected AudioSettings AudioSettings;
        protected IBorderCrosser BorderCrosser;
        protected MemoryPool<Projectile> ProjectilePool;

        [Inject]
        private void Construct
        (
            MemoryPool<Projectile> projectilePool,
            MemoryPool<AudioSource> audioPool,
            IBorderCrosser borderCrosser,
            AudioSettings audioSettings
        )
        {
            ProjectilePool = projectilePool;
            AudioPool = audioPool;
            BorderCrosser = borderCrosser;
            AudioSettings = audioSettings;
        }

        public abstract void Fire();

        protected void Despawn(Projectile sender)
        {
            ProjectilePool.Despawn(sender);
            sender.gameObject.SetActive(false);
        }
    }
}