using Enum;
using UnityEngine;

namespace EventBus.SystemEvent
{
    public struct I8NEvent:IEvent
    {
        public int Id => 1003;
        public MonoBehaviour Context;
        public LangCode LangCode;
    }
}