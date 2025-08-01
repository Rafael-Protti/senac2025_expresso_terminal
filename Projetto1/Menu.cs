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

        private bool creditos = false;

        public override void Awake()
        {
            visible = true;
            input = true;
        }
        public override void Update()
        {
            BotoesMenu();
        }

        public override void Draw()
        {
            if (!creditos)
            {
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
               
            }
            else
            {
                Creditos();
            }

        }
        private void BotoesMenu()
        {
            if (!input) { return; }

            var botao = Console.ReadKey (true);
            switch (botao.Key)
            {
                case ConsoleKey.J: //redireciona para a gameplay
                    GameManager GM = GameManager.Instancia;
                    GM.menu.visible = false;
                    GM.mapa.visible = true;
                    GM.mapa.trem.visible = true;
                    GM.mapa.trem.input = true;
                    GM.menu.input = false;
                    Console.Clear();
                    Stop();
                    break;
                case ConsoleKey.K: //redireciona para créditos
                    input = false;
                    creditos = true;
                    break;
                case ConsoleKey.L: //fecha o jogo
                    GameManager.Instancia.Stop();
                    Stop();
                    break;
            }
        }
        private void Creditos()
        {
            Console.Clear ();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Professor: Marcius");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Auxiliador: Alexandre Sant Ana Cavaleiro");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Criador: Rafael Protti");
            Console.ResetColor();
            Console.WriteLine("\nAperte qualquer tecla para voltar.");
            Console.ReadKey(true);
            creditos = false;
            input = true;
            Console.Clear();

        }
    }
}
