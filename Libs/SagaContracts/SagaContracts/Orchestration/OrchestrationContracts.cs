using System;

namespace SagaContracts.Orchestration;

/// <summary>
/// Команда запуска процесса активации. Отправляется ActivationService -> Orchestrator
/// </summary>
public interface StartActivation
{
    Guid ActivationId { get; }
    Guid CardId { get; }
}

/// <summary>
/// Команда оркестратора, требующая заблокировать карту
/// </summary>
public interface BlockCard
{
    Guid ActivationId { get; }
    Guid CardId { get; }
}

/// <summary>
/// Команда оркестратора, требующая подтвердить активацию
/// </summary>
public interface ConfirmActivation
{
    Guid ActivationId { get; }
    Guid CardId { get; }
}

/// <summary>
/// Команда оркестратора, требующая активировать карту
/// </summary>
public interface ActivateCard
{
    Guid ActivationId { get; }
    Guid CardId { get; }
}

/// <summary>
/// Событие, публикуемое CardService после блокировки карты.
/// </summary>
public interface CardBlocked
{
    Guid ActivationId { get; }
    Guid CardId { get; }
}

/// <summary>
/// Событие, публикуемое ActivationService после подтверждения активации.
/// </summary>
public interface ActivationConfirmed
{
    Guid ActivationId { get; }
    Guid CardId { get; }
}

/// <summary>
/// Событие, публикуемое CardService после окончательной активации карты.
/// </summary>
public interface CardActivated
{
    Guid ActivationId { get; }
    Guid CardId { get; }
}