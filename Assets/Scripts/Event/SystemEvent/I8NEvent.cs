using Enum;
using UnityEngine;

namespace Event.SystemEvent
{
    public struct I8NEvent:IEvent
    {
        public enum CommandType
        {
            RefreshAllLocalizedText,
            SwitchLanguage
        }

        public int Id => 1003;
        public MonoBehaviour Context;
        public readonly LangCode LangCode;

        public readonly CommandType Command;
        public I8NEvent(MonoBehaviour context,CommandType command,LangCode langCode)
        {
            Command = command;
            Context = context;
            LangCode = langCode;
        }
    }
}