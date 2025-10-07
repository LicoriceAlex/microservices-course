using Dal.Models;

namespace Dal.Repositories.Interfaces;

/// <summary>
/// Репозиторий для работы с номиналами
/// </summary>
public interface IDenominationRepository
{
    /// <summary>
    /// Получить все номиналы
    /// </summary>
    Task<List<DenominationDal>> GetAllAsync();

    /// <summary>
    /// Получить номинал по идентификатору
    /// </summary>
    Task<DenominationDal?> GetByIdAsync(Guid id);

    /// <summary>
    /// Создать новый номинал
    /// </summary>
    Task<Guid> CreateAsync(DenominationDal denominationDal);

    /// <summary>
    /// Обновить номинал
    /// </summary>
    Task UpdateAsync(DenominationDal denominationDal);

    /// <summary>
    /// Удалить номинал по идентификатору
    /// </summary>
    Task DeleteAsync(Guid id);
}