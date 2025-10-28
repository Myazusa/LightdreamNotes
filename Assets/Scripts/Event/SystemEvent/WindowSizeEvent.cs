namespace Event.SystemEvent
{
    public class WindowSizeEvent:IEvent
    {
        public enum CommandType
        {
            Size720,
            Size1080,
            SwitchFullScreen
        }
        public int Id => 103;
        public readonly CommandType Command;
    }
}