using Dal.Models;

namespace Dal.Repositories.Interfaces;

/// <summary>
/// Репозиторий для работы с вендорами
/// </summary>
public interface IVendorRepository
{
    /// <summary>
    /// Получить всех вендоров
    /// </summary>
    Task<List<VendorDal>> GetAllAsync();

    /// <summary>
    /// Получить вендора по идентификатору
    ///</summary>
    Task<VendorDal?> GetByIdAsync(Guid id);

    /// <summary>
    /// Создать нового вендора
    /// </summary>
    Task<Guid> CreateAsync(VendorDal vendorDal);

    /// <summary>
    /// Обновить данные вендора
    /// </summary>
    Task UpdateAsync(VendorDal vendorDal);

    /// <summary>
    /// Удалить вендора по идентификатору
    /// </summary>
    Task DeleteAsync(Guid id);
}