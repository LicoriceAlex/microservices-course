using System;

namespace Services.ActivatedCards.Models;

/// <summary>
/// Запрос на выборку активированных карточек пользователя
/// </summary>
public sealed class ActivatedCardsQuery
{
    // Идентификатор пользователя чьи карточки нужно вернуть
    public Guid UserId { get; set; }

    // Смещение для пагинации
    public int Offset { get; set; } = 0;

    // Размер страницы для пагинации
    public int Limit { get; set; } = 50;

    // Фильтр по статусу карточки например active blocked expired
    public string? Status { get; set; }

    // Минимальная дата активации включительно в UTC
    public DateTime? ActivatedFromUtc { get; set; }

    // Максимальная дата активации включительно в UTC
    public DateTime? ActivatedToUtc { get; set; }
}