using Azure.Identity;
using Microsoft.Extensions.Configuration.Json;

namespace Web.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static void ConfigureKeyVault(this IConfigurationBuilder config)
        {
            string? keyVaultEndpoint = Environment.GetEnvironmentVariable("KEYVAULT_ENDPOINT");

            if (keyVaultEndpoint == null)
            {
                throw new InvalidOperationException("Store the Key Vault endpoint in a KEYVAULT_ENDPOINT environment variable");
            }

            config.AddAzureKeyVault(
                new Uri($"https://{keyVaultEndpoint}.vault.azure.net/"),
                new DefaultAzureCredential(new DefaultAzureCredentialOptions
                {
                    ManagedIdentityClientId = Environment.GetEnvironmentVariable("AZURE_CLIENT_ID")
                }));
        }

        public static void WriteConfigurationSources(this IConfigurationBuilder config)
        {
            Console.WriteLine("Configuration sources\n=====================");
            foreach (var source in config.Sources)
            {
                if (source is JsonConfigurationSource jsonSource)
                    Console.WriteLine($"{source}: {jsonSource.Path}");
                else
                    Console.WriteLine(source.ToString());
            }
            Console.WriteLine("=====================\n");
        }
    }
}
