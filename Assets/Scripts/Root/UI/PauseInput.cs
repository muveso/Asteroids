using System;
using UnityEngine;

namespace Root.UI
{
    public class PauseInput : MonoBehaviour
    {
        [SerializeField] private GameObject menu;
        public Action<bool> OnPause = _ => { };

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            var scale = Time.timeScale > 0 ? 0 : 1;
            var isZero = scale == 0;
            menu.SetActive(isZero);
            OnPause.Invoke(!isZero);
            Time.timeScale = scale;
        }
    }
}