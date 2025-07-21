using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetto1
{
    internal class Menu
    {
        public bool jogorodando = true;
        public Menu()
        {
            do
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

            } while (jogorodando);
            Console.Clear ();
            Console.WriteLine("Obrigado por jogar!");
        }
        private void BotoesMenu()
        {
            var botao = Console.ReadKey (true);
            switch (botao.Key)
            {
                case ConsoleKey.J: //redireciona para a gameplay
                    break;
                case ConsoleKey.K: //redireciona para créditos
                    Creditos();
                    break;
                case ConsoleKey.L: //fecha o jogo
                    jogorodando = false;
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
