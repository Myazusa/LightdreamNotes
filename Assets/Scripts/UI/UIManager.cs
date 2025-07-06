using System.Collections.Generic;
using System.Linq;
using Enum;
using UnityEngine.UIElements;

namespace UI
{
    public class UIManager
    {
        public VisualElement RootVisualElement;

        private readonly Dictionary<UILayerName, Stack<VisualElement>> _uiLayers = new()
        {
            { UILayerName.Base, new Stack<VisualElement>() },
            { UILayerName.Main, new Stack<VisualElement>() },
            { UILayerName.Talk, new Stack<VisualElement>() },
            { UILayerName.Dialog, new Stack<VisualElement>() },
            { UILayerName.Info, new Stack<VisualElement>() }
        };

        private readonly Dictionary<UILayerName, VisualElement> _uiLayouts = new();

        /// <summary>
        /// 添加UI元素到指定栈中
        /// </summary>
        /// <param name="layerName">栈的名字</param>
        /// <param name="view">UI元素</param>
        public void PushElement(UILayerName layerName, VisualElement view)
        {
            var stack = _uiLayers[layerName];

            // 若栈顶有元素就隐藏
            if (_uiLayers[layerName].Count > 0) stack.Peek().visible = false;

            // 然后添加当前元素进去
            stack.Push(view);
        }

        public VisualElement GetTopElement(UILayerName layerName)
        {
            var stack = _uiLayers[layerName];
            return stack.Peek();
        }

        /// <summary>
        /// 移除指定栈中的UI元素
        /// </summary>
        /// <param name="layerName">栈的名字</param>
        public void PopElement(UILayerName layerName)
        {
            var stack = _uiLayers[layerName];

            // 没有元素，返回
            if (stack.Count == 0) return;

            // 弹出最顶部元素后显示下级元素
            stack.Pop();
            stack.Peek().visible = true;
        }

        /// <summary>
        /// 移除指定栈中所有的UI元素
        /// </summary>
        /// <param name="layerName">栈的名字</param>
        public void PopAllElements(UILayerName layerName)
        {
            var stack = _uiLayers[layerName];

            if (stack.Count == 0) return;

            stack.Clear();
        }

        /// <summary>
        /// 在场景加载时再初始化UI
        /// </summary>
        public void InitLayers()
        {

            var layerOrder = _uiLayers.Select(keyValuePair => keyValuePair.Key).ToList();

            foreach (var layerName in layerOrder)
            {
                var container = new VisualElement
                {
                    name = $"Layer_{layerName}",
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
                RootVisualElement.Add(container);
                _uiLayouts[layerName] = container;
            }
        }
    }
}