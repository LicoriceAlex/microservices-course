using System;

namespace SagaContracts.Choreography;

/// <summary>
/// Событие о создании активации, публикуемое ActivationService
/// </summary>
public interface ActivationInitiated
{
    Guid ActivationId { get; }
    Guid CardId { get; }
}

/// <summary>
/// Событие о блокировке карты, публикуемое CardService
/// </summary>
public interface CardBlocked
{
    Guid ActivationId { get; }
    Guid CardId { get; }
}

/// <summary>
/// Событие, публикуемое ActivationService после подтверждения активации пользователем
/// </summary>
public interface ActivationConfirmed
{
    Guid ActivationId { get; }
    Guid CardId { get; }
}

/// <summary>
/// Событие, публикуемое CardService после окончательной активации карты
/// </summary>
public interface CardActivated
{
    Guid ActivationId { get; }
    Guid CardId { get; }
}