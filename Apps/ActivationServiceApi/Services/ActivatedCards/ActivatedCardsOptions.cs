namespace Services.ActivatedCards;

/// <summary>
/// Настройки сервиса ActivatedCards
/// </summary>
public sealed class ActivatedCardsOptions
{
    // Базовый адрес сервиса карточек например https://cards.internal
    public string? BaseAddress { get; set; }

    // Путь endpoint получения активированных карточек
    public string ActivatedCardsPath { get; set; } = "/api/cards/activated";

    // Имя именованного HttpClient если используется преднастройка клиента
    public string? ClientName { get; set; }

    // Таймаут запросов в секундах
    public int? DefaultTimeoutSeconds { get; set; }
}