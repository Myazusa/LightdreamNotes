namespace Event.SystemEvent
{
    public struct ScreenSizeEvent:IEvent
    {
        public enum CommandType
        {
            Size720,
            Size900,
            Size1080,
            SwitchFullScreen
        }
        public int Id => 103;
        public readonly CommandType Command;
    }
}