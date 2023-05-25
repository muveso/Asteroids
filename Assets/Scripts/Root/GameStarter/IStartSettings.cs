using System.Collections.Generic;
using UnityEngine;

namespace Root.GameStarter
{
    public interface IStartSettings
    {
        RectTransform StartText { get; }

        List<GameObject> GameObjects { get; }
    }
}