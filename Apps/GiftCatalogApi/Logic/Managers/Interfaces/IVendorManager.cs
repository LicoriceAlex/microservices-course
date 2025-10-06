using Dal.Models;

namespace Logic.Managers.Interfaces;

/// <summary>
/// Интерфейс менеджера бизнес-логики для работы с вендорами.
/// </summary>
public interface IVendorManager
{
    /// <summary>Получить всех вендоров.</summary>
    Task<List<VendorDal>> GetAllAsync();

    /// <summary>Получить вендора по идентификатору.</summary>
    Task<VendorDal?> GetByIdAsync(Guid id);

    /// <summary>Создать нового вендора.</summary>
    Task<Guid> CreateAsync(string name, string slug, bool isActive = true);

    /// <summary>Обновить данные существующего вендора.</summary>
    Task UpdateAsync(Guid id, string name, string slug, bool isActive);

    /// <summary>Удалить вендора по идентификатору.</summary>
    Task DeleteAsync(Guid id);
}