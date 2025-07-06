using Event;
using Event.SystemEvent;
using UnityEngine;

namespace Scene
{
    public class StartMenuScene : MonoBehaviour
    {
        public void OnStartButtonClick()
        {
            // 指定事件触发的handle函数
            var e = new SceneEvent(this,SceneEvent.CommandType.ToMainGameScene);
            // 触发传递事件并触发handle
            EventBus.Instance.TryPost(ref e);
        }

        public void OnQuitButtonClick()
        {
            var e = new SceneEvent(this,SceneEvent.CommandType.GameQuit);
            EventBus.Instance.TryPost(ref e);
        }
    }
}

