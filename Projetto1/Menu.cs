using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetto1
{
    public class Menu : MonoBehaviour
    {
        private bool creditos = false;
        private Menu()
        {
            Run();
        }
        private static Menu instancia;
        static public Menu Instancia => instancia ??= new Menu();

        public override void Awake()
        {
            //visible = true;
        }
        public override void Update()
        {
            BotoesMenu();
        }

        public override void Draw()
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
            Console.WriteLine();
            Creditos();
        }
        private void BotoesMenu()
        {
            if (!input) { return; }

            var botao = Console.ReadKey (true);
            switch (botao.Key)
            {
                case ConsoleKey.J: //redireciona para a gameplay
                    //GameManager.Instancia.jogando = true;
                    visible = false;
                    Console.Clear();
                    Stop();
                    break;
                case ConsoleKey.K: //redireciona para créditos
                    creditos = true;
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
            if (creditos == true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Instrutor: Marcius \nCriador: Rafael Protti");
                Console.ResetColor();
                Console.WriteLine("\nAperte qualquer tecla para voltar.");
                Console.ReadKey(true);
                creditos = false;
            }

        }
    }
}
