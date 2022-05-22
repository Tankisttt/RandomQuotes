using System.Configuration;

namespace RandomQuotes.Sdk.Configuration
{
    public class RandomQuotesApiConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty(nameof(RandomQuotesApiBaseUrl), IsRequired = true)]
        public string RandomQuotesApiBaseUrl => (string)this[nameof(RandomQuotesApiBaseUrl)];
    }
}