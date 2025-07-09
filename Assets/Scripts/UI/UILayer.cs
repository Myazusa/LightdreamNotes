using System.Collections.Generic;
using Enum;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class UILayer
    {
        private int _topNumber;
        private readonly GameObject _rootGameObject;
        private readonly List<GameObject> _uiObjects = new();
        // 这是映射，因为同一个sortingOrder可能有多个ui
        private readonly Dictionary<string, int> _uiMap = new();

        public UILayer(GameObject canvas,UILayerName layerName)
        {
            _topNumber = 0;
            _rootGameObject = new GameObject(layerName.ToString());
            _rootGameObject.transform.SetParent(canvas.transform);

        }

        public void AddUIElement(string visualTreeAssetName)
        {
            // todo: 记得设置这个物件为UI层
            var gameObject = new GameObject(visualTreeAssetName);
            gameObject.transform.SetParent(_rootGameObject.transform);
            _uiObjects.Add(gameObject);

            // 添加UIDocument组件并读取uxml资源赋予它
            var uiDocument = gameObject.AddComponent<UIDocument>();
            var visualTreeAsset = Resources.Load<VisualTreeAsset>(visualTreeAssetName);
            uiDocument.visualTreeAsset = visualTreeAsset;
            // 设置它的排序为最高并添加到映射
            uiDocument.sortingOrder = _topNumber;
            _uiMap.Add(visualTreeAssetName, _topNumber);
            // 启用它
            uiDocument.enabled = true;
            // 设置panelSetting
            uiDocument.panelSettings = null;

            _topNumber++;
        }

        public void RemoveUIElement()
        {
            var visualTreeAssetName = _uiObjects[_uiMap.Count - 1].name;
            _uiMap.Remove(visualTreeAssetName);
            _uiObjects.RemoveAt(_uiObjects.Count - 1);

            _topNumber--;
        }
    }
}