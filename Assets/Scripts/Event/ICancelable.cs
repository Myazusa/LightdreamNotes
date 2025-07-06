namespace Event
{
    public interface ICancelable
    {
        bool IsCancelled { get; }
        void Cancel();
    }
}