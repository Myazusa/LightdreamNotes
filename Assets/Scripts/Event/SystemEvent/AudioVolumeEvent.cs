using UnityEngine;

namespace Event.SystemEvent
{
    public struct AudioVolumeEvent:IEvent
    {
        public enum CommandType
        {
            // 这里不可变更，是和MainMixmer混响器资源对应的
            Master,
            BgmVolume,
            SfxVolume,
            VoiceVolume,
        }
        public int Id => 102;
        public float Volume;
        public readonly CommandType Command;
    }
}