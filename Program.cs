using static GeradorSenha.Tela;

namespace GeradorSenha
{
    public class Program
    {

        public static void Main(string[] args)
        {

            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                Encerrar();
            };

            MenuPrincipal();
            System.Threading.Thread.Sleep(1000);
            Encerrar();

        }

    }
}