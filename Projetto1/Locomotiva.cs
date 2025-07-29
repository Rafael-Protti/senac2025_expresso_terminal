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

        public int playerX;
        public int playerY;
        string trem = "        ____ __  _______ |[]|-||_ |_____|-|_____(_) o=o=o  00=OO=o/\\";
        public int tremX = 17;
        public int tremY = 4;
        public int velocidade = 0;
        public bool embaixo = false;
        public int locomocao;
        public int combustivel = 10000;

        public Vector2 pos = new Vector2(1, 1);

        public Locomotiva()
        {
            Run();
        }
        public override void Draw()
        {
            
            for (int y = 0; y < tremY; y++) //desenha a locomotiva
            {
                for (int x = 0; x < tremX; x++)
                {
                    Console.SetCursorPosition(pos.x+x, pos.y+y);
                    Console.Write(trem[y * tremX + x]);
                }
            }
        }
        public void AtualizarPosicao(ConsoleKey tecla)
        {
            int oldx = pos.x;
            int oldy = pos.y;
            int x = pos.x;
            int y = pos.y;

            if (!input) { return; }

            if (velocidade < 60) {x = pos.Right;} // locomotiva se move sozinha. Substituir pela função de velocidade.
            if (velocidade >= 60 && velocidade <= 90) {x = pos.Right + 10;}

            switch (tecla)
            {
                case ConsoleKey.A:
                    DiminuirVelocidade();
                    break;
                case ConsoleKey.D:
                    AumentarVelocidade();
                    break;
            }



            //se mov for valido
            //   pos.x = nova posiçaõ
            //else
            //   nao passa 
        }

        private void Movimento()
        {
            if (velocidade == 0) { locomocao = 0; }
            if (velocidade >= 5 && velocidade <= 25)
            {
                locomocao = 1; //andar 1 espaço
            }
            if (velocidade >= 26 && velocidade <= 55)
            {
                locomocao = 2; //andar 2 espaços
            }
            if (velocidade >= 56 && velocidade <= 75)
            {
                locomocao = 3; //andar 3 espaços
            }
            if (velocidade >= 76 && velocidade <= 95)
            {
                locomocao = 4; //andar 4 espaços
            }
            if (velocidade >= 96 && velocidade <= 120)
            {
                locomocao = 5; //andar 5 espaços
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
            if (!embaixo)
            {
                embaixo = true;
                pos.y = 7;
            }
            else
            { 
                embaixo = false;
                pos.y = 2;
            }
        }
        private void GastoCombustivel()
        {
            combustivel -= locomocao * 10;
            if (playerX <= 50 && playerX >= 40 && playerY == 2 && velocidade > 30)
            { combustivel = combustivel - 2000; }
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
