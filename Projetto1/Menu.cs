using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetto1
{
    public class Menu : MonoBehaviour
    {
        private Menu()
        {
            Run();
        }
        private static Menu instancia;
        static public Menu Instancia => instancia ??= new Menu();
        public override void Update()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("""
                .-----------------.
                |EXPRESSO TERMINAL|
                º-----------------°
                 .---------------.
                 |-> Jogar    [J]|
                 |-> Créditos [K]|
                 |-> Sair     [L]|
                 °---------------°
                """);
            Console.ResetColor();
            BotoesMenu();
        }
        private void BotoesMenu()
        {
            var botao = Console.ReadKey (true);
            switch (botao.Key)
            {
                case ConsoleKey.J: //redireciona para a gameplay
                    GameManager.Instancia.jogando = true;
                    break;
                case ConsoleKey.K: //redireciona para créditos
                    Creditos();
                    break;
                case ConsoleKey.L: //fecha o jogo
                    GameManager.Instancia.Stop();
                    Stop();
                    break;
            }
        }
        private void Creditos()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Instrutor: Marcius \nCriador: Rafael Protti");
            Console.ResetColor();
            Console.WriteLine("\nAperte qualquer tecla para voltar.");
            Console.ReadKey();
        }
    }
}
