using System;
using System.IO;
using System.Security.Cryptography;

namespace ClassLib
{
    public interface IEncryptor
    {
        byte[] Encrypt(byte[] data);
        byte[] Decrypt(byte[] data);
    }

    public class Encryptor : IEncryptor
    {
        private readonly byte[] key;
        private readonly byte[] iv;

        public Encryptor(byte[] key, byte[] iv)
        {
            if (key.Length != 16 && key.Length != 24 && key.Length != 32)
                throw new ArgumentException("Key size must be 16, 24, or 32 bytes.");
            this.key = key;
            this.iv = iv;
        }

        public byte[] Encrypt(byte[] data)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    return PerformCryptography(data, encryptor);
                }
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    return PerformCryptography(data, decryptor);
                }
            }
        }


        private byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(data, 0, data.Length);
                        cryptoStream.FlushFinalBlock();
                        return ms.ToArray();
                    }
                }
            }
            catch (CryptographicException ex)
            {
                throw new InvalidOperationException("An error occurred during cryptographic operation", ex);
            }
        }
    }
}

