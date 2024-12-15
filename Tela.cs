namespace GeradorSenha
{
    public static class Tela
    {


        private static int _Escape = 2;
        private static int _Altura = 20;
        private static int _Horizontal = 60;
        private static string _LinhaBase = $"║{"".PadLeft(_Horizontal, ' ')}║";

        public static string Texto(string texto)
        {

            string textoFinal = $"{_LinhaBase.Substring(0, 2)}{texto}{_LinhaBase.Substring(texto.Length + 2)}";
            return textoFinal;

        }

        public static string Ler(int linha, string pergunta, string padrao = "")
        {

            Imprimir(linha, pergunta);
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(pergunta.Length + _Escape + 2, linha);
            string resposta = Console.ReadLine() ?? "";
            Console.ResetColor();
            return resposta.Equals("") ? padrao : resposta;

        }



        public static void ImprimirSair(int linha, string texto)
        {
            ImpirmirNoLocal(linha, texto);
            Console.SetCursorPosition(0, _Altura + 1);
        }

        public static void Imprimir(int linha, string texto)
        {
            ImpirmirNoLocal(linha, Texto(texto));
        }


        public static void ImpirmirNoLocal(int linha, string texto)
        {
            Console.SetCursorPosition(0, linha);
            Console.Write(new string(' ', _Escape));
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(texto);
            Console.ResetColor();
            Console.Write("");
        }
        public static void DesenharTela(string titulo)
        {
            Console.Clear();
            Console.SetCursorPosition(0, Console.CursorTop);

            string linhaHorizontalSuperior = $"╔{new string('═', _Horizontal)}╗";
            string linhaHorizontalInferior = $"╚{new string('═', _Horizontal)}╝";
            int tamanhoTitulo = titulo.Length;
            int inicioPos = ((_Horizontal / 2) - (tamanhoTitulo / 2));
            int finalPos = (inicioPos + tamanhoTitulo);

            string linhaTitulo = $"{_LinhaBase.Substring(0, inicioPos)}{titulo}{_LinhaBase.Substring(finalPos)}";

            for (int iLinha = 0; iLinha < _Altura; iLinha++)
            {

                if (iLinha == 0)
                {
                    ImprimirSair(iLinha, linhaHorizontalSuperior);
                    continue;
                }
                if (iLinha == 1)
                {
                    ImprimirSair(iLinha, linhaTitulo);
                    continue;
                }
                ImprimirSair(iLinha, _LinhaBase);
                Console.WriteLine("");
            }

            ImprimirSair(_Altura, linhaHorizontalInferior);
            Console.WriteLine("");

        }


        public static void MenuPrincipal()
        {
            DesenharTela("GERENCIADOR DE SENHA");
            int iLinha = 3;
            Imprimir(iLinha++, "1) Gerar senha");
            Imprimir(iLinha++, "2) Abrir cofre");
            Imprimir(iLinha++, "3) Adicionar valor ao cofre");
            Imprimir(iLinha++, "4) Fechar cofre");
            Imprimir(iLinha++, "0) Sair");
            string resposta = Ler(iLinha++, "Escolha uma opcao? ", "0");
            switch (resposta)
            {
                case "1":
                    new GeradorSenha().Init();
                    break;
                case "2":
                    new Cofre().Abrir();
                    break;
                case "3":
                    new Cofre().Gravar();
                    break;
                case "4":
                case "0":
                default:
                    Encerrar();
                    break;
            }
        }

        public static void Encerrar()
        {
            Console.ResetColor();
            Console.Clear();
            Environment.Exit(0);
        }


    }
}