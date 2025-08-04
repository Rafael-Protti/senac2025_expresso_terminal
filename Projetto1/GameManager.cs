using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Projetto1
{
    public class GameManager : MonoBehaviour
    {
        private GameManager()
        {
            Run();
        }
        private static GameManager instancia;
        static public GameManager Instancia => instancia ??= new GameManager(); //manter atributos abaixo do singleton

        public bool jogando = false;
        public Menu menu = Menu.Instancia;
        public Mapa mapa = Mapa.Instancia;



        public override void Awake()
        {

        }
        public override void Update()
        {
            Console.SetCursorPosition(0, 0);
            Draw();
        }

        public override void Draw()
        {
            if (menu.visible) { menu.Draw(); }
            if (mapa.visible) { mapa.Draw(); }
        }

        public override void LateUpdate()
        {
            mapa.RedesenharMapa();
            if (mapa.locomotiva.recursos.carga <= 0)
            {
                mapa.Stop();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("""
                Você derrubou toda a carga.
                Fim de jogo!
                """);
                Console.ResetColor();
                Stop();
            }
            if (mapa.locomotiva.recursos.combustivel <= 0)
            {
                mapa.Stop();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("""
                Seu combustível acabou.
                Fim de jogo!
                """);
                Stop();
            }
            if (mapa.locomotiva.recursos.carga <= 0 && mapa.locomotiva.combporcento <= 0)
            {
                mapa.Stop();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("""
                Toda a carga foi derrubada e todo combustível foi gasto. Que incompetência.
                Fim de jogo!
                """);
                Stop();
            }
            if (mapa.locomotiva.distancia == 165 * 3)
            {
                mapa.Stop();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"""
                Parabéns! Você conseguiu chegar na estação.
                Carga entregue (ton): {mapa.locomotiva.recursos.carga}
                Combustivel retante (%): {mapa.locomotiva.combporcento}
                Fim de jogo!
                """);
                Stop();
            }
        }
        public override void OnDestroy()
        {
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Obrigado por jogar!");
        }



    }

}
