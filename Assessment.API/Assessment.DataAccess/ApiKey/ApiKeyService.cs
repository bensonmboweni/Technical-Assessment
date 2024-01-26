using Assessment.DataAccess.ApiKey.IApiKey.IApiKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.DataAccess.ApiKey
{
    public class ApiKeyService: IApiKey.IApiKey.IApiKeyService
    {
        private const string _prefix = "Assessment-";
        private const int _numberOfSecureBytesToGenerate = 100;
        private const int _lengthOfKey = 100;

        public string GenerateApiKey()
        {
            byte[] bytes = RandomNumberGenerator.GetBytes(_numberOfSecureBytesToGenerate);

            string base64String = Convert.ToBase64String(bytes)
                .Replace("+", "-")
                .Replace("/", "_");

            int keyLength = _lengthOfKey - _prefix.Length;

            return _prefix + base64String[..keyLength];
        }
    }
}
