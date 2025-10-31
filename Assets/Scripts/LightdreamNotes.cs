using System;
using Enum;
using Event;
using Event.SystemEvent;
using Event.SystemEventHandler;
using UnityEngine;


public class LightdreamNotes:MonoBehaviour
{
    public static LightdreamNotes Instance { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        // 初始化事件总线
        InitEventBus();
    }
    private void Awake()
    {
        // 如果已有实例，销毁自己
        if (ReferenceEquals(Instance,null) && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private static void InitEventBus()
    {
        Debug.Log("开始初始化事件总线");
        // 注册所有事件
        EventBus.Instance.Register<SceneEvent>(EventPriority.Lowest,SceneEventHandler.HandleToMainGameScene);
        EventBus.Instance.Register<SceneEvent>(EventPriority.Lowest,SceneEventHandler.HandleGameQuit);

        EventBus.Instance.Register<AudioVolumeEvent>(EventPriority.Lowest,GameSettingEventHandler.HandleChangeVolume);
        EventBus.Instance.Register<ScreenSizeEvent>(EventPriority.Lowest, GameSettingEventHandler.HandleSetWindowSize);
        EventBus.Instance.Register<ScreenSizeEvent>(EventPriority.Lowest, GameSettingEventHandler.HandleSwitchFullScreen);

    }
}
