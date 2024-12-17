using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.Base
{
    public static class Security
    {
        public static string EncryptString(string message)
        {
            byte[] results;
            UTF8Encoding utf8 = new UTF8Encoding();

            // Create the hash provider and generate a key
            using (var hashProvider = MD5.Create()) // Use the base Create method
            {
                byte[] tdesKey = hashProvider.ComputeHash(utf8.GetBytes("Al-Ma$r1"));//

                // Use TripleDES algorithm with a key
                using (var tdesAlgorithm = TripleDES.Create()) // Use the base Create method
                {
                    tdesAlgorithm.Key = tdesKey;
                    tdesAlgorithm.Mode = CipherMode.ECB;
                    tdesAlgorithm.Padding = PaddingMode.PKCS7;

                    byte[] dataToEncrypt = utf8.GetBytes(message);

                    // Perform encryption
                    using (ICryptoTransform encryptor = tdesAlgorithm.CreateEncryptor())
                    {
                        results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
                    }
                }
            }

            return Convert.ToBase64String(results);
        }

        public static string DecryptString(string Message)
        {
            byte[] Results;
            UTF8Encoding UTF8 = new UTF8Encoding();
            using(var hashProvider = MD5.Create())
            {
                byte[] TDESKey = hashProvider.ComputeHash(UTF8.GetBytes("Al-Ma$r1"));

                using(var tdesAlgorithm = TripleDES.Create())
                {
                    tdesAlgorithm.Key = TDESKey;
                    tdesAlgorithm.Mode = CipherMode.ECB;
                    tdesAlgorithm.Padding= PaddingMode.PKCS7;

                    string urlDecoded = HttpUtility.UrlDecode(Message);
                    byte[] DataToDecrypt;

                    try
                    {
                        if (urlDecoded.Length >= 12)
                            DataToDecrypt = Convert.FromBase64String(Message);
                        else
                            return Message;
                        ICryptoTransform Decryptor = tdesAlgorithm.CreateDecryptor();
                        Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
                    }
                    catch (Exception)
                    {
                        return Message;
                    }
                    finally
                    {
                        tdesAlgorithm.Clear();
                        hashProvider.Clear();
                    }
                }
            }
            
            return UTF8.GetString(Results);
        }
    }
}
