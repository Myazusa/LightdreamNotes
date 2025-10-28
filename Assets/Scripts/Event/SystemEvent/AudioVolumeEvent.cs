using UnityEngine;

namespace Event.SystemEvent
{
    public class AudioVolumeEvent:IEvent
    {
        public enum CommandType
        {
            MasterVolume,
            BgmVolume,
            SfxVolume,
            VoiceVolume,
        }
        public int Id => 102;
        public float Volume;
        public readonly CommandType Command;
    }
}