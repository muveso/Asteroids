using Root.Constants;
using UnityEngine;
using Zenject;
using AudioSettings = Root.Settings.AudioSettings;

namespace Root.Player
{
    public class EngineSound
    {
        private readonly AudioSource _audioSource;

        public EngineSound(MemoryPool<AudioSource> audioPool, AudioSettings audioSettings)
        {
            _audioSource = audioPool.Spawn();
            _audioSource.clip = audioSettings.AudioStorage[AudioConstants.Thrust];
        }

        public void PlaySound(float vertical)
        {
            if (vertical > 0) _audioSource.Play();
            else _audioSource.Stop();
        }
    }
}