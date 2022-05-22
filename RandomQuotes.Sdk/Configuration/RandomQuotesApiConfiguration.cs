using System;
using System.Configuration;

namespace RandomQuotes.Sdk.Configuration
{
    public class RandomQuotesApiConfiguration
    {
        private const string RandomQuotesConfigSectionName = "RandomQuotesApiConfiguration";
        private const string RandomQuotesConfigSectionError = "RandomQuotesConfiguration not found.";

        static RandomQuotesApiConfiguration()
        {
            Configuration = (RandomQuotesApiConfigurationSection)ConfigurationManager.GetSection(RandomQuotesConfigSectionName);
            if (Configuration == null)
                throw new Exception(RandomQuotesConfigSectionError);
        }

        public static RandomQuotesApiConfigurationSection Configuration { get; }
    }
}