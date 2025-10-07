namespace Domain.Enums;

/// <summary>
/// статус активации
/// </summary>
public enum ActivationStatus
{
    /// <summary>
    /// Ожидает
    /// </summary>
    Pending = 0,
    
    /// <summary>
    /// Подтвержден
    /// </summary>
    Confirmed = 1,
    
    /// <summary>
    /// Аннулирован
    /// </summary>
    Canceled = 2,
    
    /// <summary>
    /// Неуспешен
    /// </summary>
    Failed = 3
}