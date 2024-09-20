using IndustryX.MessageBridge.MessageBridge.Core.Factories;
using IndustryX.MessageBridge.MessageBridge.Core.Interfaces;

namespace IndustryX.MessageBridge.MessageBridge.Core.Services
{
    public class QueuedHostedService : BackgroundService
    {
        private readonly IBackgroundTaskQueue _taskQueue;

        public QueuedHostedService(IBackgroundTaskQueue taskQueue)
        {
            _taskQueue = taskQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Kuyruktan iş çekiyoruz
                var workItem = await _taskQueue.DequeueAsync(stoppingToken);

                try
                {
                    // Test için 10 saniye bekle
                    await Task.Delay(10000, stoppingToken);


                    // Kuyruktan alınan iş çalıştırılıyor (mesaj gönderiliyor)
                    await workItem(stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                }
            }
        }
    }


}
