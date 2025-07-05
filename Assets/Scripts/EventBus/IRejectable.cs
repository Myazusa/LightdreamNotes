namespace EventBus
{
    public interface IRejectable
    {
        bool IsRejected { get; }
        void Reject();
    }
}