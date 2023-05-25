using System;
using System.Collections.Generic;
using Root.Utils;
using UnityEngine;
using Zenject;

namespace Root.Settings
{
    [Serializable]
    public class AudioSettings : IInitializable
    {
        [SerializeField] private List<Pair<string, AudioClip>> audioStorage;

        [field: SerializeField] public AudioSource AudioSourcePrefab { get; private set; }

        public Dictionary<string, AudioClip> AudioStorage { get; private set; } = new();

        public void Initialize()
        {
            if (AudioStorage.Count > 0) return;
            foreach (var audio in audioStorage) AudioStorage.Add(audio.Key, audio.Value);
        }
    }
}