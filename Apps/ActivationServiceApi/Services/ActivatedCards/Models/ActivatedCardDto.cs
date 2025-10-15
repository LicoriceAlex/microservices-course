using System;

namespace Services.ActivatedCards.Models;

/// <summary>
/// Карточка, активированная пользователем
/// </summary>
public sealed class ActivatedCardDto
{
    // Идентификатор карточки
    public Guid CardId { get; set; }

    // Отображаемый номер карточки без конфиденциальных данных
    public string? PublicNumber { get; set; }

    // Дата активации в UTC
    public DateTime ActivatedAtUtc { get; set; }

    // Статус карточки
    public string? Status { get; set; }
}