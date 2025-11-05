namespace CoreLib.DistributedSync.Abstractions;

public interface IDistributedSemaphore
{
    /// <summary>
    /// Имя
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Максимальное колличество
    /// </summary>
    int MaxCount { get; }

    /// <summary>
    /// Попытка взять билет семафора в течение timeout
    /// </summary>
    ValueTask<IDistributedSemaphoreHandle?> TryAcquireAsync(TimeSpan timeout, CancellationToken cancellationToken = default);

    /// <summary>
    /// Гарантированно взять или кинуть TimeoutException
    /// </summary>
    ValueTask<IDistributedSemaphoreHandle> AcquireAsync(TimeSpan timeout, CancellationToken cancellationToken = default);

    /// <summary>
    /// Доступное количество (после очистки просрочки)
    /// </summary>
    ValueTask<int> GetCurrentCountAsync(CancellationToken cancellationToken = default);
}

