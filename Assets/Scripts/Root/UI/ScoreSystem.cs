using System;
using System.Collections.Generic;
using Root.Enemy;
using Root.Settings;

namespace Root.UI
{
    public class ScoreSystem : IScoreSystem
    {
        private readonly Dictionary<EnemyType, int> _scoreDictionary = new();

        public ScoreSystem(GameSettings gameSettings)
        {
            var scoreList = gameSettings.ScoreList;
            foreach (var score in scoreList) _scoreDictionary.Add(score.Key, score.Value);
        }

        public event Action OnChanged = () => { };

        public void AddScore(EnemyType type)
        {
            Score += _scoreDictionary[type];
            OnChanged.Invoke();
        }

        public int Score { get; private set; }
    }
}