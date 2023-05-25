using System;
using Root.Enemy;

namespace Root.UI
{
    public interface IScoreSystem
    {
        int Score { get; }
        event Action OnChanged;
        void AddScore(EnemyType type);
    }
}