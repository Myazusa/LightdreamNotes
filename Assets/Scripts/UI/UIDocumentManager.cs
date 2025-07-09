
using System.Collections.Generic;
using Enum;
using UnityEngine;

namespace UI
{
    public class UIDocumentManager : MonoBehaviour
    {
        private readonly List<UILayer> _uiLayers = new(4);
        private GameObject _canvas;

        private void OnEnable()
        {
            // todo: 记得设置物件为UI层
            _canvas = GameObject.Find("Canvas");
            // todo: 记得设置UI所属层，不然这些层级之间是没有顺序的
            _uiLayers.Add(new UILayer(_canvas,UILayerName.Base));
            _uiLayers.Add(new UILayer(_canvas,UILayerName.Main));
            _uiLayers.Add(new UILayer(_canvas,UILayerName.Talk));
            _uiLayers.Add(new UILayer(_canvas,UILayerName.Dialog));
            _uiLayers.Add(new UILayer(_canvas,UILayerName.Info));
        }
    }
}