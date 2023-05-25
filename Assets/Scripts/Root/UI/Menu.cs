using Root.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Root.UI
{
    public class Menu : MonoBehaviour
    {
        private static bool _notFirstStart;
        [SerializeField] private TextMeshProUGUI schemeText;
        [SerializeField] private PauseInput pauseInput;
        [SerializeField] private GameObject gameStarter;
        [SerializeField] private Button[] buttons;

        private IInputSettings _inputSettings;

        private void Start()
        {
            Time.timeScale = 1;
            if (_notFirstStart)
            {
                gameStarter.SetActive(true);
                gameObject.SetActive(false);
            }

            buttons[0].onClick.AddListener(Resume);
            buttons[0].gameObject.SetActive(false);
            buttons[1].onClick.AddListener(StartGame);
            buttons[2].onClick.AddListener(ChangeInputScheme);
            buttons[3].onClick.AddListener(Application.Quit);
        }

        private void OnEnable()
        {
            buttons[0].gameObject.SetActive(true);
            schemeText.text = _inputSettings.InputScheme.ToString();
        }

        [Inject]
        private void Construct(IInputSettings inputSettings)
        {
            _inputSettings = inputSettings;
        }

        public void SetupRestart()
        {
            Time.timeScale = 0;
            pauseInput.OnPause.Invoke(false);
            pauseInput.gameObject.SetActive(false);
            buttons[0].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(false);
        }

        private void ChangeInputScheme()
        {
            var inputScheme = _inputSettings.InputScheme;
            inputScheme = inputScheme == InputScheme.Keyboard
                ? InputScheme.KeyboardMouse
                : InputScheme.Keyboard;

            _inputSettings.InputScheme = inputScheme;

            schemeText.text = inputScheme.ToString();
        }

        private void StartGame()
        {
            if (_notFirstStart) SceneManager.LoadScene(0);

            _notFirstStart = true;
            gameStarter.SetActive(true);
            gameObject.SetActive(false);
        }

        private void Resume()
        {
            Time.timeScale = 1;
            pauseInput.OnPause.Invoke(true);
            gameObject.SetActive(false);
        }
    }
}