using System.Security.Cryptography;
using System.Text;

namespace Logic.Utilities;

/// <summary>
/// Вспомогательные методы для генерации и хэширования кодов
/// </summary>
public static class CodeTools
{
    /// <summary>
    /// Сгенерировать простой код карты (16 алфанумерических символов)
    /// </summary>
    public static string GenerateCode()
    {
        return Guid.NewGuid().ToString("N")[..16].ToUpperInvariant();
    }

    /// <summary>
    /// Посчитать SHA256-хэш и получить маску вида ****-****-****-1234.
    /// </summary>
    public static (string Hash, string Mask) HashAndMask(string plain)
    {
        using SHA256 sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(plain));
        var hash = Convert.ToHexString(bytes);

        var tail = plain.Length >= 4 ? plain[^4..] : plain;
        var mask = $"****-****-****-{tail}";

        return (hash, mask);
    }
}