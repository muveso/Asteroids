using System;
using UnityEngine;
using Zenject;

namespace Root.GameStarter
{
    /// <summary>
    /// Listens for input to start the game and activates game objects.
    /// </summary>
    public class StarterInput : MonoBehaviour, IStarter
    {
        private IStartSettings _startSettings;

        private void Start()
        {
            _startSettings.StartText.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (!Input.anyKey) return;
            _startSettings.GameObjects.ForEach(x => x.SetActive(true));
            OnGameStart.Invoke();
            enabled = false;
        }

        /// <summary>
        /// Invoked when the game starts.
        /// </summary>
        public event Action OnGameStart = () => { };

        [Inject]
        private void Construct(IStartSettings startSettings)
        {
            _startSettings = startSettings;
        }
    }
}