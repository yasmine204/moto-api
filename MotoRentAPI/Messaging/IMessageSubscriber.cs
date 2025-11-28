namespace MotoRentAPI.Messaging
{
    public interface IMessageSubscriber
    {
        void Subscribe<T>(Func<T, Task> handler);
    }
}
