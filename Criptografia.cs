using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace GeradorSenha
{

    public class Criptografa
    {
        public Criptografa()
        {
            string filePath = "seu_arquivo.txt";
            string encryptedFilePath = "seu_arquivo.txt.enc";
            string password = "minha_senha_forte";

            // Gera a chave e IV
            using (var aes = Aes.Create())
            {
                var key = GenerateKey(password, aes.KeySize / 8);
                aes.Key = key;
                aes.GenerateIV();

                // Criptografa o arquivo
                EncryptFile(filePath, encryptedFilePath, aes.Key, aes.IV);
                Console.WriteLine("Arquivo criptografado com sucesso!");
            }
        }

        private byte[] GenerateKey(string password, int keySize)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                return sha256.ComputeHash(passwordBytes, 0, keySize);
            }
        }

        private void EncryptFile(string inputFile, string outputFile, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (var encryptor = aes.CreateEncryptor())
                using (var inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                using (var outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    // Escreve o IV no início do arquivo criptografado
                    outputStream.Write(iv, 0, iv.Length);

                    using (var cryptoStream = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write))
                    {
                        inputStream.CopyTo(cryptoStream);
                    }
                }
            }
        }
    }

}