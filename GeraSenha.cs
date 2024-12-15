using static GeradorSenha.Tela;

namespace GeradorSenha
{

    public class GeradorSenha
    {

        private const string _Lowercase = "abcdefghijklmnopqrstuvwxyz";
        private const string _Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string _Numbers = "01234567890123456789012345";
        private const string _SpecialChars = "!@%_;:,.!@%_;:,.!@%_;:,.!@";

        public string Gerar(string senha)
        {
            return senha;
        }

        public void Init()
        {
            DesenharTela("GERADOR DE SENHA");

            string resposta = Ler(3, "Qual o tamanho da senha? ") ?? throw new ArgumentNullException("Valor Inválido");
            int tamanho = int.Parse(resposta);
            int iLinha = 4;
            Imprimir(iLinha++, $"Senha terá {resposta} caracteres");


            Random random = new Random();


            string todos = $"{_Lowercase}{_SpecialChars}{_Numbers}{_Uppercase}{_SpecialChars}{_Numbers}xxxx";
            string senhaTodos = "";
            for (int i = 0; i < tamanho; i++)
            {
                int iPosicao = random.Next(1, todos.Length - 3);
                senhaTodos += todos.Substring(iPosicao, 1);
            }

            Imprimir(iLinha++, $"Senha: {senhaTodos}");

            string letraNumeros = $"{_Lowercase}{_Numbers}{_Uppercase}{_Numbers}";
            string senhaLetraNumeros = "";
            for (int i = 0; i < tamanho; i++)
            {
                int iPosicao = random.Next(1, letraNumeros.Length - 3);
                senhaLetraNumeros += letraNumeros.Substring(iPosicao, 1);
            }

            Imprimir(iLinha++, $"Senha: {senhaLetraNumeros}");

            string numeros = $"{_Numbers}";
            string senhaNumeros = "";
            for (int i = 0; i < tamanho; i++)
            {
                int iPosicao = random.Next(1, numeros.Length - 3);
                senhaNumeros += numeros.Substring(iPosicao, 1);
            }

            Imprimir(iLinha++, $"Senha: {senhaNumeros}");

            Ler(++iLinha, "Enter para voltar para tela principal");
            MenuPrincipal();

        }
    }

}