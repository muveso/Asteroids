using System;
using UnityEngine;

namespace Root.Utils
{
    [Serializable]
    public class Pair<TKey, TValue>
    {
        [field: SerializeField] public TKey Key { get; private set; }

        [field: SerializeField] public TValue Value { get; private set; }
    }
}