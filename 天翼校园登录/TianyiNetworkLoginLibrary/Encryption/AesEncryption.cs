using System.Security.Cryptography;
using System.Text;

namespace TianyiNetworkLoginLibrary.Encryption
{
    /// <summary>
    /// AES加密解密
    /// </summary>
    class AesEncryption
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">密钥 长度为16的倍数</param>
        /// <param name="iv">iv偏移量 长度为16的倍数</param>
        public AesEncryption(string key, string iv)
        {
            _key = Encoding.UTF8.GetBytes(key);
            _iv = Encoding.UTF8.GetBytes(iv);
        }

        public string EncryptStringToBase64(string plainText)
        {
            byte[] encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _key;
                aesAlg.IV = _iv;
                // 创建解密器
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                // 创建用于加密的流
                using MemoryStream ms = new MemoryStream();
                using CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                }
                encrypted = ms.ToArray();
            }
            return Convert.ToBase64String(encrypted);
        }

        public string DecryptStringFromBase64(string encryptedText)
        {
            string plaintext;// 储存明文
            byte[] cipherText = Convert.FromBase64String(encryptedText);
            // 使用指定的KEY和IV创建Aes对象
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _key;
                aesAlg.IV = _iv;
                // 创建解密器
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                // 创建用于解密的流
                using MemoryStream ms = new MemoryStream(cipherText);
                using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using StreamReader sr = new StreamReader(cs);
                // 读取解密后的文本
                plaintext = sr.ReadToEnd();
            }
            return plaintext;
        }
    }
}
