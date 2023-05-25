using System;
using UnityEngine;
using Zenject;

namespace Root.Settings
{
    [Serializable]
    public class BoundarySettings : IBoundarySettings, IInitializable
    {
        [field: SerializeField] public RectTransform Playground { get; private set; }

        public Vector3[] CornerPosition { get; } = new Vector3[4];

        public void Initialize()
        {
            Playground.GetWorldCorners(CornerPosition);
        }
    }
}