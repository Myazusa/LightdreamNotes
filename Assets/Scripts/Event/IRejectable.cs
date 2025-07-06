namespace Event
{
    public interface IRejectable
    {
        bool IsRejected { get; }
        void Reject();
    }
}