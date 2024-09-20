using System.Threading.Channels;
using IndustryX.MessageBridge.MessageBridge.Core.Interfaces;

namespace IndustryX.MessageBridge.MessageBridge.API
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<Func<CancellationToken, Task>> _queue;

        public BackgroundTaskQueue(int capacity = 100)
        {
            // Kapasitesi ayarlanabilir bir queue oluşturuyoruz
            _queue = Channel.CreateBounded<Func<CancellationToken, Task>>(capacity);
        }

        public void QueueMessage(Func<CancellationToken, Task> workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            _queue.Writer.TryWrite(workItem);
        }

        public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            var workItem = await _queue.Reader.ReadAsync(cancellationToken);
            return workItem;
        }
    }
}
