using System;

namespace RandomQuotes.Resources;

public static class BCryptTools
{
    public static string CreatePasswordSalt() => BCrypt.Net.BCrypt.GenerateSalt();

    public static string CreatePasswordHash(string password, string salt)
    {
        if (password is null)
            throw new ArgumentNullException(nameof(password));

        if (salt is null)
            throw new ArgumentNullException(nameof(salt));

        return BCrypt.Net.BCrypt.HashPassword(inputKey: password, salt: salt);
    }
}