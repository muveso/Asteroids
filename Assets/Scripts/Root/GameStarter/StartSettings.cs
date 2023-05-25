using System;
using System.Collections.Generic;
using UnityEngine;

namespace Root.GameStarter
{
    /// <summary>
    /// Contains settings for the start panel and the game objects to activate when the game starts.
    /// </summary>
    [Serializable]
    public class StartSettings : IStartSettings
    {
        /// <summary>
        /// The RectTransform of the start text to animate.
        /// </summary>
        [field: SerializeField] public RectTransform StartText { get; private set; }

        /// <summary>
        /// The list of game objects to activate when the game starts.
        /// </summary>
        [field: SerializeField] public List<GameObject> GameObjects { get; private set; }
    }
}