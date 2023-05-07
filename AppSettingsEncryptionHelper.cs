using Glimpse;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace apiSocialWeb
{
    public class AppSettingsEncryptionHelper
    {
        public static void EncryptAppSettings(string filePath)
        {
            IDataProtectionProvider dataProtectionProvider = DataProtectionProvider.Create("apiSocialWeb");

            IDataProtector dataProtector = dataProtectionProvider.CreateProtector("MyAppSettings");

            string file = File.ReadAllText(filePath);

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(file, settings);

            string connectionString = jsonObject.ConnectionString.Connection;

            string encryptedConnectionString = dataProtector.Protect(connectionString);

            jsonObject.ConnectionString.Connection = encryptedConnectionString;

            string outputJsonString = JsonConvert.SerializeObject(jsonObject, settings);

            File.WriteAllText(filePath, outputJsonString);
        }

        public static void DecryptAppSettings(string filePath)
        {
            IDataProtectionProvider dataProtectionProvider = DataProtectionProvider.Create("apiSocialWeb");

            IDataProtector dataProtector = dataProtectionProvider.CreateProtector("MyAppSettings");

            string file = File.ReadAllText(filePath);

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(file, settings);

            string encryptedConnectionString = jsonObject.ConnectionString.Connection;

            string decryptedConnectionString = dataProtector.Unprotect(encryptedConnectionString);

            jsonObject.ConnectionString.Connection = decryptedConnectionString;

            string outputJsonString = JsonConvert.SerializeObject(jsonObject, settings);

            File.WriteAllText(filePath, outputJsonString);
        }

        public static string GetConnectionString(string filePath)
        {
            string file = File.ReadAllText(filePath);

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(file, settings);

            string connectionString = jsonObject.ConnectionString.Connection;

            return connectionString;

        }
    }
}
