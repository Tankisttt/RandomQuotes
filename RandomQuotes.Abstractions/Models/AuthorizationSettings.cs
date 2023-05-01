using System;

namespace RandomQuotes.Abstractions.Models;

public class AuthorizationSettings
{
    public string Secret { get; set; }

    public TimeSpan TokenLifetime { get; set; }
}