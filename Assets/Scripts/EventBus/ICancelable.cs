namespace EventBus
{
    public interface ICancelable
    {
        bool IsCancelled { get; }
        void Cancel();
    }
}