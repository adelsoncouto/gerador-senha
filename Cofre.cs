using static GeradorSenha.Tela;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace GeradorSenha
{
    public class Cofre
    {

        private string _Arquivo = Path.Combine(AppContext.BaseDirectory, "cofre.enc");


        public void Gravar()
        {
            DesenharTela("COFRE");
            string senha = Ler(3, "Informe a senha do cofre: ");
            string continua = "S";
            List<string> valores = new List<string>();
            do
            {
                string valor = GravarIncluir(senha);
                valores.Add(valor);
                DesenharTela("COFRE");
                continua = Ler(3, "Gravar outro valor (S/n)? ", "S");
            } while (continua.Equals("S", StringComparison.OrdinalIgnoreCase));

            string dados = string.Join('\n', valores);

             byte[] salt = GerarSalt();
            (byte[] chave, byte[] iv) = DerivarChaveEIV(senha, salt, 1000);

            byte[] mensagem = Criptografar(dados, chave, iv);

            File.WriteAllText(_Arquivo, Convert.ToBase64String(mensagem));

            MenuPrincipal();

        }



        public string GravarIncluir(string senha)
        {
            DesenharTela("COFRE");
            string chave = Ler(3, "Informe a chave: ");
            string valor = Ler(4, "Informe o valor: ");
            Imprimir(10, $"{chave}: {valor}");
            return $"{chave}: {valor}";
        }




        public void Abrir()
        {
            DesenharTela("COFRE");

            string senha = Ler(3, "Informe a senha: ");
            byte[] mensagem = File.ReadAllBytes(_Arquivo);
            byte[] salt = GerarSalt();
            (byte[] chave, byte[] iv) = DerivarChaveEIV(senha, salt, 1000);
            string mensagemDescriptografada = Descriptografar(mensagem, chave, iv);
            Imprimir(5, mensagemDescriptografada);

            // byte[] mensagemCriptografada = Criptografar(mensagemOriginal, chave, iv);
            // Console.WriteLine("Mensagem Criptografada (Base64): " + Convert.ToBase64String(mensagemCriptografada));



            // Console.WriteLine("Mensagem Descriptografada: " + mensagemDescriptografada);


            // File.WriteAllText(_Arquivo, Convert.ToBase64String(mensagemCriptografada));

            string resposta = Ler(8, "Cofre aberto");
            MenuPrincipal();

        }


        private byte[] Criptografar(string textoPlano, byte[] chave, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = chave;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(textoPlano);
                        }
                        return ms.ToArray();
                    }
                }
            }
        }

        private string Descriptografar(byte[] textoCriptografado, byte[] chave, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = chave;
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(textoCriptografado))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        private (byte[] chave, byte[] iv) DerivarChaveEIV(string senha, byte[] salt, int iterations)
        {
            const int keySize = 32; // Tamanho da chave (256 bits)
            const int ivSize = 16;  // Tamanho do IV (128 bits)

            using (var rfc2898 = new Rfc2898DeriveBytes(
                senha,
                salt,
                iterations,
                HashAlgorithmName.SHA256)) // Recomenda-se usar SHA256
            {
                byte[] chave = rfc2898.GetBytes(keySize);
                byte[] iv = rfc2898.GetBytes(ivSize);
                return (chave, iv);
            }
        }

        private byte[] GerarSalt()
        {
            const int saltSize = 16; // Tamanho do salt (128 bits)
            byte[] salt = new byte[saltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }


    }
}




