using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Projetto1
{
    class Locomotiva : MonoBehaviour
    {
        //private Locomotiva() { }
        //private static Locomotiva instancia;
        //static public Locomotiva Instancia => instancia??=new Locomotiva();
        //Pixel trem = new Pixel();
        string trem = "        ____ __  _______ |[]|-||_ |_____|-|_____(_) o=o=o  00=OO=o/\\";
        public int playerX;
        public int playerY;
        public int tremX = 17;
        public int tremY = 4;
        public int velocidade = 0;
        public bool embaixo = false;
        public int locomocao;
        public int combustivel = 10000;

        public Vector2 pos = new Vector2(1, 2);

        public Locomotiva()
        {
            Run();
        }
        public override void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            
            for (int y = 0; y < tremY; y++) //desenha a locomotiva
            {
                for (int x = 0; x < tremX; x++)
                {
                    Console.SetCursorPosition(pos.x+x, pos.y+y);
                    Console.Write(trem[y * tremX + x]);
                }
            }
            Console.ResetColor();
        }
        public void AtualizarPosicao(ConsoleKey tecla)
        {
            int oldx = pos.x;
            int oldy = pos.y;
            int x = pos.x;
            int y = pos.y;

            if (!input) { return; }

            switch (tecla)
            {
                case ConsoleKey.A:
                    DiminuirVelocidade();
                    break;
                case ConsoleKey.D:
                    AumentarVelocidade();
                    break;
                case ConsoleKey.W:
                    embaixo = true;
                    TrocarTrilho();
                    break;
                case ConsoleKey.S:
                    embaixo = false;
                    TrocarTrilho();
                    break;
            }
        }

        public void Movimento()
        {
            locomocao = velocidade / 12;
            if (velocidade > 0)
            {
                pos.x = pos.Right + locomocao; // controla a velocidade da locomotiva
                GastoCombustivel();
            }
            
        }
        private void AumentarVelocidade()
        {
            if (velocidade < 120) { velocidade += 5; }
        }
        private void DiminuirVelocidade()
        {
            if (velocidade > 0) { velocidade -= 5; }
        }

        private void TrocarTrilho()
        {
            if (velocidade > 0)
            {
                combustivel -= 100;
                if (!embaixo) { pos.y = 7; }
                else { pos.y = 2; }
            }
        }
        private void GastoCombustivel()
        {
            combustivel -= locomocao * 10;
        }

        public override void Update()
        {
            
        }

        public override void LateUpdate()
        {
            var tecla = Console.ReadKey(true).Key;
            AtualizarPosicao(tecla);
        }
    }
}
