using System;
using System.Collections.Generic;
using System.Linq;
using Enum;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    [Obsolete(message:"使用UIDocumentManager.cs来管理层级")]
    public class UIElementManager
    {
        private readonly VisualElement _rootVisualElement;

        /// <summary>
        /// 层级名一共5层，下面挂着的是一个uxml，这只是逻辑上的进出栈，单纯进出栈不会影响屏幕上的变化
        /// </summary>
        private readonly Dictionary<UILayerName, Stack<TemplateContainer>> _uiLayers = new()
        {
            { UILayerName.Base, new Stack<TemplateContainer>() },
            { UILayerName.Main, new Stack<TemplateContainer>() },
            { UILayerName.Talk, new Stack<TemplateContainer>() },
            { UILayerName.Dialog, new Stack<TemplateContainer>() },
            { UILayerName.Info, new Stack<TemplateContainer>() }
        };

        /// <summary>
        /// 这里存的是每一层所创建的实际的根的Element，它是这样的
        /// root
        ///  - rootBase
        ///  - rootMain
        ///  - rootTalk
        ///  - rootDialog
        ///  - rootInfo
        /// 之后所有的uxml，都会以TemplateContainer的形式挂载在这些根下面
        /// </summary>
        private readonly Dictionary<UILayerName, VisualElement> _uiLayerLayout = new();

        public UIElementManager(VisualElement rootVisualElement)
        {
            _rootVisualElement = rootVisualElement;
            // todo: 记得初始化base的栈底元素
        }

        /// <summary>
        /// 添加UI元素到指定栈中并显示它
        /// </summary>
        /// <param name="layerName">要加入哪个层</param>
        /// <param name="visualTreeAssetName">要加载的UI的uxml的名字</param>
        /// <param name="hideLowerLayer">是否要隐藏处于下级的层，即隐藏Base、Main等等这种层</param>
        public void PushElementAndAddToScreen(UILayerName layerName,string visualTreeAssetName,bool hideLowerLayer = true)
        {
            // 若栈顶有元素就隐藏
            HideLowerElementsAndLayer(layerName, hideLowerLayer);

            var container = LoadTemplateContainer(visualTreeAssetName);

            // 然后添加当前元素进去
            var stack = _uiLayers[layerName];
            stack.Push(container);

            // 添加到屏幕上
            AddElementToScreen(container, layerName);
        }

        /// <summary>
        /// 移除指定栈中的UI元素并且从屏幕上移除
        /// </summary>
        /// <param name="layerName">栈的名字</param>
        public void PopElement(UILayerName layerName)
        {
            var stack = _uiLayers[layerName];

            // 栈底，返回
            if (stack.Count == 0) return;
            if (layerName == UILayerName.Base && stack.Count <= 1) return;

            // 移除屏幕->出栈->显示下级
            RemoveElementToScreen(layerName);
            stack.Pop();
            ShowLowerElementsAndLayer(layerName);
        }

        /// <summary>
        /// 移除指定栈中所有的UI元素，并从屏幕上移除
        /// </summary>
        /// <param name="layerName">栈的名字</param>
        public void PopAllElements(UILayerName layerName)
        {
            var stack = _uiLayers[layerName];

            // 栈底，返回
            if (stack.Count == 0) return;
            if (layerName == UILayerName.Base && stack.Count <= 1) return;

            var endCount = 0;
            if (layerName == UILayerName.Base)
            {
                endCount = 1;
            }
            while (stack.Count == endCount)
            {
                // 先移除屏幕
                RemoveElementToScreen(layerName);
                // 再出栈
                stack.Pop();
            }
            // 用来显示下一个层
            ShowLowerElementsAndLayer(layerName);

        }

        /// <summary>
        /// 移除所有栈中元素直到Base的栈底元素（Base的栈底元素不可移除）
        /// </summary>
        public void PopAllElements()
        {
            foreach (var layer in _uiLayers)
            {
                if (layer.Key == UILayerName.Base && layer.Value.Count == 1)
                {
                    continue;
                }
                if (layer.Key != UILayerName.Base && layer.Value.Count == 0)
                {
                    continue;
                }
                var endCount = layer.Key == UILayerName.Base ? 1 : 0;
                {
                    // 循环出栈
                    while (layer.Value.Count != endCount)
                    {
                        RemoveElementToScreen(layer.Key);
                        layer.Value.Pop();
                    }
                }
            }
            // 一定会到Base的栈底，因此如果没有显示就显示它
            _uiLayerLayout[UILayerName.Base].visible = true;
        }

        /// <summary>
        /// 在场景加载时再初始化UI
        /// </summary>
        public void InitLayers(VisualTreeAsset baseVisualTreeAsset)
        {

            var layerOrder = _uiLayers.Select(keyValuePair => keyValuePair.Key).ToList();

            foreach (var layerName in layerOrder)
            {
                var layerLayout = new VisualElement
                {
                    name = $"Layer{layerName}",
                    // 容器本身不拦截事件
                    pickingMode = PickingMode.Ignore,
                    style =
                    {
                        position = Position.Absolute,
                        top = 0,
                        bottom = 0,
                        left = 0,
                        right = 0,
                        flexGrow = 1
                    }
                };

                // 添加到画面上
                _rootVisualElement.Add(layerLayout);
                _uiLayerLayout[layerName] = layerLayout;
            }
        }

        private TemplateContainer LoadTemplateContainer(string visualTreeAssetName)
        {
            var asset = Resources.Load<VisualTreeAsset>(visualTreeAssetName);
            if (!asset) return null;
            var container = asset.Instantiate();
            return container;
        }

        public static VisualTreeAsset LoadVisualTreeAsset(string visualTreeAssetName)
        {
            return Resources.Load<VisualTreeAsset>(visualTreeAssetName);
        }

        /// <summary>
        /// 推入元素前使用它
        /// </summary>
        /// <param name="layerName"></param>
        /// <param name="hideLowerLayer"></param>
        private void HideLowerElementsAndLayer(UILayerName layerName,bool hideLowerLayer = true)
        {
            // 如果需要隐藏处于下级的层
            if (hideLowerLayer)
            {
                switch (layerName)
                {
                    case UILayerName.Base: break;
                    case UILayerName.Main:
                    {
                        _uiLayerLayout[UILayerName.Base].visible = false;
                        break;
                    }
                    case UILayerName.Talk:
                    {
                        _uiLayerLayout[UILayerName.Base].visible = false;
                        _uiLayerLayout[UILayerName.Main].visible = false;
                        break;
                    }
                    case UILayerName.Dialog:
                    {
                        _uiLayerLayout[UILayerName.Base].visible = false;
                        _uiLayerLayout[UILayerName.Main].visible = false;
                        _uiLayerLayout[UILayerName.Talk].visible = false;
                        break;
                    }
                    case UILayerName.Info:
                    {
                        _uiLayerLayout[UILayerName.Base].visible = false;
                        _uiLayerLayout[UILayerName.Main].visible = false;
                        _uiLayerLayout[UILayerName.Talk].visible = false;
                        _uiLayerLayout[UILayerName.Dialog].visible = false;
                        break;
                    }
                    default:
                    {
                        Debug.LogWarning("Unknown layer name");
                        break;
                    }
                }
            }

            // 不需要就隐藏当前层处于下级的元素即可
            _uiLayers[layerName].Peek().visible = false;
        }

        /// <summary>
        /// 必须先让当前的元素出栈才可以调用该方法，也就是一个占用前台显示的UI点击关闭后让它出栈，才能调用这个方法
        /// </summary>
        /// <param name="layerName"></param>
        private void ShowLowerElementsAndLayer(UILayerName layerName)
        {
            // 如果当前层归零，要显示处于下面的层
            if (_uiLayers[layerName].Count == 0)
            {
                // 判断是哪层
                switch (layerName)
                {
                    case UILayerName.Base:
                    {
                        break;
                    }
                    case UILayerName.Main:
                    {
                        _uiLayerLayout[UILayerName.Base].visible = true;
                        break;
                    }
                    case UILayerName.Talk:
                    {
                        _uiLayerLayout[UILayerName.Main].visible = true;
                        break;
                    }
                    case UILayerName.Dialog:
                    {
                        _uiLayerLayout[UILayerName.Talk].visible = true;
                        break;
                    }
                    case UILayerName.Info:
                    {
                        _uiLayerLayout[UILayerName.Dialog].visible = true;
                        break;
                    }
                    default:
                    {
                        Debug.LogWarning("Unknown layer name");
                        break;
                    }
                }
                // 因为为空栈了，就直接返回了
                return;
            }
            // 如果栈里还有东西，那就设置当前这个可见
            _uiLayers[layerName].Peek().visible = true;
        }

        /// <summary>
        /// 必须在出栈前使用
        /// </summary>
        /// <param name="layerName"></param>
        private void RemoveElementToScreen(UILayerName layerName)
        {
            var container = _uiLayers[layerName].Peek();
            _uiLayerLayout[layerName].Remove(container);
        }

        private void AddElementToScreen(TemplateContainer container, UILayerName layerName)
        {
            // 添加到画面上
            _uiLayerLayout[layerName].Add(container);
        }
    }
}