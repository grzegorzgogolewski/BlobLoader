using System;
using System.Security.Cryptography;
using System.Text;

namespace BlobLoader
{
    public static class SecureText
    {
        private static readonly byte[] Entropy = Encoding.Unicode.GetBytes("GISNET*Grzegorz*Gogolewski");

        public static string Protect(string input)
        {
            byte[] encryptedData = ProtectedData.Protect(Encoding.Unicode.GetBytes(input), Entropy, DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(encryptedData);
        }

        public static string UnProtect(string encryptedData)
        {
            try
            {
                byte[] decryptedData = ProtectedData.Unprotect(Convert.FromBase64String(encryptedData), Entropy, DataProtectionScope.CurrentUser);

                return Encoding.Unicode.GetString(decryptedData);
            }
            catch
            {
                return string.Empty;
            }
        }
    }

}
