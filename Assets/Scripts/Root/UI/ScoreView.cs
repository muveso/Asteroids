using System;
using TMPro;

namespace Root.UI
{
    public class ScoreView : IDisposable
    {
        private readonly TextMeshProUGUI _score;

        private readonly IScoreSystem _scoreSystem;

        public ScoreView(TextMeshProUGUI score, IScoreSystem scoreSystem)
        {
            _score = score;
            _scoreSystem = scoreSystem;
            _scoreSystem.OnChanged += UpdateText;
        }

        public void Dispose()
        {
            _scoreSystem.OnChanged -= UpdateText;
        }

        private void UpdateText()
        {
            _score.text = $"Score {_scoreSystem.Score}";
        }
    }
}