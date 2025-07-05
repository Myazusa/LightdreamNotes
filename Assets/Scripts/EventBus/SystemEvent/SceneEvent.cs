using UnityEngine;

namespace EventBus.SystemEvent
{
    public struct SceneEvent:IEvent
    {
        public int Id => 1002;
        public MonoBehaviour Context;
    }
}