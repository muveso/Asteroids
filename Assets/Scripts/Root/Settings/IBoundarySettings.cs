using UnityEngine;

namespace Root.Settings
{
    public interface IBoundarySettings
    {
        RectTransform Playground { get; }

        Vector3[] CornerPosition { get; }
    }
}