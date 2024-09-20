namespace IndustryX.MessageBridge.MessageBridge.Core.Interfaces
{
    public interface IBackgroundTaskQueue
    {
        void QueueMessage(Func<CancellationToken, Task> workItem);
        Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
    }
}
