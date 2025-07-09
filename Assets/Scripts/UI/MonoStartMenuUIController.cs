using System;
using Event;
using Event.SystemEvent;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class MonoStartMenuUIController : MonoBehaviour
    {
        private VisualElement _root;
        private Button _startButton;
        private Button _settingsButton;
        private Button _exitButton;

        private const string BASE_UXML_NAME = "StartMenu";

        private UIElementManager _elementManager;

        private void Awake()
        {
            // 这个脚本要和UIDocument组件挂在同一个物件上
            var uiDocument = GetComponent<UIDocument>();
            _root = uiDocument.rootVisualElement;
            // todo: 这里是不对的，栈底和根不是同一个元素
            _elementManager = new UIElementManager(_root);
            var asset = UIElementManager.LoadVisualTreeAsset(BASE_UXML_NAME);
            if (asset)
            {
                _elementManager.InitLayers(asset);
            }
            else
            {
                Debug.LogError($"Failed to load " + BASE_UXML_NAME);
            }
        }
        private void OnEnable()
        {
            // 所有的事件放在这个周期注册是为了可以统一的禁用UI

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
            // 不启用就卸载事件
            _startButton = _root.Q<Button>("start-menu-start-button");
            _startButton.clicked -= OnStartButtonClicked;

            _settingsButton = _root.Q<Button>("start-menu-setting-button");
            _startButton.clicked -= OnSettingButtonClicked;

            _exitButton = _root.Q<Button>("start-menu-exit-button");
            _exitButton.clicked -= OnExitButtonClicked;
        }

        private void OnDestroy()
        {
            _elementManager = null;
        }
    }
}

