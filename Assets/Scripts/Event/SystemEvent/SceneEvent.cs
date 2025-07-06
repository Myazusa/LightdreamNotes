using UnityEngine;

namespace Event.SystemEvent
{
    public struct SceneEvent:IEvent
    {
        public enum CommandType
        {
            ToMainGameScene,
            GameQuit
        }
        public int Id => 1002;
        public readonly MonoBehaviour Context;
        public readonly CommandType Command;
        public SceneEvent(MonoBehaviour context,CommandType command)
        {
            Command = command;
            Context = context;
        }
    }
}