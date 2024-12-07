namespace GeradorSenha
{
    public class Program
    {


        private int _CursorLinhaAtual = 1;
        private int _Escape = 5;

        private const string _Lowercase = "abcdefghijklmnopqrstuvwxyz";
        private const string _Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string _Numbers =      "01234567890123456789012345";
        private const string _SpecialChars = "!@%_;:,.!@%_;:,.!@%_;:,.!@";

        public static void Main(string[] args)
        {
            var programa = new Program();
            programa.init();
        }

        public void init()
        {
            Console.Clear();
            _CursorLinhaAtual = Console.CursorTop;
            DesenharTela();

            Imprimir(1, Texto("     GERADOR DE SENHA"));
            string resposta = Ler(3, Texto("Qual o tamanho da senha? ")) ?? throw new ArgumentNullException("Valor Inválido");
            int tamanho = int.Parse(resposta);
            int iLinha = 4;
            Imprimir(iLinha++, Texto($"Senha terá {resposta} caracteres"));


            Random random = new Random();


            string todos = $"{_Lowercase}{_SpecialChars}{_Numbers}{_Uppercase}{_SpecialChars}{_Numbers}xxxx";
            string senhaTodos = "";
            for (int i = 0; i < tamanho; i++)
            {
                int iPosicao = random.Next(1, todos.Length - 3);
                senhaTodos += todos.Substring(iPosicao, 1);
            }

            Imprimir(iLinha++, Texto($"Senha: {senhaTodos}"));

            string letraNumeros = $"{_Lowercase}{_Numbers}{_Uppercase}{_Numbers}";
            string senhaLetraNumeros = "";
            for (int i = 0; i < tamanho; i++)
            {
                int iPosicao = random.Next(1, letraNumeros.Length - 3);
                senhaLetraNumeros += letraNumeros.Substring(iPosicao, 1);
            }

            Imprimir(iLinha++, Texto($"Senha: {senhaLetraNumeros}"));

            string numeros = $"{_Numbers}";
            string senhaNumeros = "";
            for (int i = 0; i < tamanho; i++)
            {
                int iPosicao = random.Next(1, numeros.Length - 3);
                senhaNumeros += numeros.Substring(iPosicao, 1);
            }

            Imprimir(iLinha++, Texto($"Senha: {senhaNumeros}"));


        }

        public string Texto(string texto)
        {
            string textoBase = $"#{new string(' ', 30)}#";

            string textoFinal = $"{textoBase.Substring(0, 2)}{texto}{textoBase.Substring(texto.Length + 2)}";

            return textoFinal;

        }

        public string? Ler(int linha, string pergunta)
        {

            ImpirmirNoLocal(linha, pergunta);
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(pergunta.Length, linha + _CursorLinhaAtual);
            string? resposta = Console.ReadLine();
            Console.ResetColor();
            return resposta;

        }



        public void Imprimir(int linha, string texto)
        {
            ImpirmirNoLocal(linha, texto);
            Console.SetCursorPosition(0, 11);
        }



        public void ImpirmirNoLocal(int linha, string texto)
        {
            Console.SetCursorPosition(0, _CursorLinhaAtual + linha);
            Console.Write(new string(' ', _Escape));
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(texto);
            Console.ResetColor();
            Console.Write("");
        }
        public void DesenharTela()
        {

            Console.SetCursorPosition(0, Console.CursorTop);
            string textoLinha = "";
            textoLinha = textoLinha.PadLeft(30);
            textoLinha = $"#{textoLinha}#";

            for (int iLinha = 0; iLinha < 10; iLinha++)
            {

                if (iLinha == 0)
                {
                    Imprimir(iLinha, new string('#', 32));
                    continue;
                }
                Imprimir(iLinha, textoLinha);
                Console.WriteLine("");
            }

            Imprimir(10, new string('#', 32));
            Console.WriteLine("");



        }

    }
}