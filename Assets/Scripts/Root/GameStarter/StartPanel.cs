using System;
using DG.Tweening;
using UnityEngine;

namespace Root.GameStarter
{
    /// <summary>
    /// Displays the start panel with a bouncing text and hides it when the game starts.
    /// </summary>
    public class StartPanel : IDisposable
    {
        private readonly IStarter _starter;
        private readonly RectTransform _text;

        public StartPanel(IStartSettings startSettings, IStarter starter)
        {
            _text = startSettings.StartText;
            _starter = starter;

            _starter.OnGameStart += DeleteText;
            _text.DOAnchorPosY(180, 1).SetLoops(-1, LoopType.Yoyo);
        }

        public void Dispose()
        {
            _starter.OnGameStart -= DeleteText;
        }

        private void DeleteText()
        {
            _text.gameObject.SetActive(false);
        }
    }
}