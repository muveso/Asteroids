using System;
using System.Collections.Generic;
using Root.Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Root.UI
{
    [Serializable]
    public class HealthPanel : IInitializable, IDisposable
    {
        [SerializeField] private PhysicsCollision physicsCollision;

        [FormerlySerializedAs("monoMenu")] [SerializeField]
        private Menu menu;

        [field: SerializeField] public List<Image> List { get; private set; }

        private int _health = 4;

        public void Dispose()
        {
            physicsCollision.OnCollision -= RemoveHeart;
        }

        public void Initialize()
        {
            physicsCollision.OnCollision += RemoveHeart;
        }

        public void RemoveHeart()
        {
            _health -= 1;
            if (_health < 0) return;
            List[_health].gameObject.SetActive(false);
            if (_health != 0) return;
            menu.gameObject.SetActive(true);
            menu.SetupRestart();
        }
    }
}