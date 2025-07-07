using Event;
using Event.SystemEvent;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class StartMenuUIController : MonoBehaviour
    {
        private VisualElement _root;
        private Button _startButton;
        private Button _settingsButton;
        private Button _exitButton;

        private void Awake()
        {
            var uiDocument = GetComponent<UIDocument>();
            _root = uiDocument.rootVisualElement;
        }
        private void OnEnable()
        {
            _startButton = _root.Q<Button>("start-menu-start-button");
            _startButton.clicked += OnStartButtonClicked;

            _settingsButton = _root.Q<Button>("start-menu-setting-button");
            _startButton.clicked += OnSettingButtonClicked;

            _exitButton = _root.Q<Button>("start-menu-exit-button");
            _exitButton.clicked += OnExitButtonClicked;
        }

        private void OnStartButtonClicked()
        {
            var e = new SceneEvent(this,SceneEvent.CommandType.ToMainGameScene);
            EventBus.Instance.TryPost(ref e);
        }

        private void OnSettingButtonClicked()
        {

        }

        private void OnExitButtonClicked()
        {
            var e = new SceneEvent(this,SceneEvent.CommandType.GameQuit);
            EventBus.Instance.TryPost(ref e);
        }
        private void OnDisable()
        {
            _startButton = _root.Q<Button>("start-menu-start-button");
            _startButton.clicked -= OnStartButtonClicked;

            _settingsButton = _root.Q<Button>("start-menu-setting-button");
            _startButton.clicked -= OnSettingButtonClicked;

            _exitButton = _root.Q<Button>("start-menu-exit-button");
            _exitButton.clicked -= OnExitButtonClicked;
        }
    }
}

