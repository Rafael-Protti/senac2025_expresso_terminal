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
    class Locomotiva
    {
        //private Locomotiva() { }
        //private static Locomotiva instancia;
        //static public Locomotiva Instancia => instancia??=new Locomotiva();

        public int playerX;
        public int playerY;
        string trem = "        ____ __   _______ |[]|-||_  |_____|-|_____(_)  o=o=o  00=OO=o/\\o";
        public int tremX = 18;
        public int tremY = 4;
        public int velocidade = 0;
        public bool embaixo = false;
        public int locomocao;
        public int combustivel = 10000;

        Vector2 pos = new Vector2(1, 1);

        public Locomotiva(int x, int y)
        {
            this.pos = new Vector2(x, y);
        }
        public void DesenharLocomotiva()
        {
            for (int y = 0; y < tremY; y++) //desenha a locomotiva
            {
                for (int x = 0; x < tremX; x++)
                {
                    mapa[playerX + x, playerY + y] = trem[y * tremX + x];
                }
                Console.WriteLine();
            }
        }
        public void AtualizarPosicao(ConsoleKey tecla)
        {

            int oldX = pos.x;
            int oldY = pos.y;
            int x = pos.x;
            int y = pos.y;

            switch (tecla)
            {
                case ConsoleKey.A:
                    if (velocidade > 0) { velocidade = velocidade - 5; };
                    break;
                case ConsoleKey.D:
                    if (velocidade < 120) { velocidade = velocidade + 5; };
                    break;
                case ConsoleKey.F:
                    Movimento();
                    oldX += locomocao;
                    GastoCombustivel();
                    break;
                case ConsoleKey.W:
                    embaixo = false;
                    //tempY--;
                    break;
                case ConsoleKey.S:
                    embaixo = true;
                    //tempY++;
                    break;
                case ConsoleKey.L:
                    jogorodando = false;
                    ValoresPadrao();
                    Main();
                    break;
                case ConsoleKey.M:
                    combustivel--;
                    break;
                case ConsoleKey.N:
                    carga--;
                    break;
            }

            if (embaixo) //posiciona a locomotiva no trilho de cima e no trilho de baixo.
            {
                oldY = 7;
            }
            else { oldY = 2; }

            if (mapa[oldX, oldY] != '#' && mapa[oldX + tremX, oldY + tremY] != '#')
            {
                for (int y = 0; y < tremY; y++)
                {
                    for (int x = 0; x < tremX; x++)
                    {
                        mapa[playerX + x, playerY + y] = ' ';
                    }
                }

                for (int x = 0; x < tremX; x++) //Redesenha o trilho no Y = 5
                {
                    mapa[playerX + x, 5] = 'I';
                }

                for (int x = 0; x < tremX; x++) //Redesenha o trilho no y = 10
                {
                    mapa[playerX + x, 10] = 'I';
                }

                for (int y = 0; y < tremY; y++)
                {
                    for (int x = 0; x < tremX; x++)
                    {
                        mapa[oldX + x, oldY + y] = trem[y * tremX + x];
                    }
                }

                playerX = oldX;
                playerY = oldY;
            }

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
                y = 7;
            }
            else
            { 
                embaixo = false;
                y = 2;
            }
        }
        private void GastoCombustivel()
        {
            combustivel -= locomocao * 10;
            if (playerX <= 50 && playerX >= 40 && playerY == 2 && velocidade > 30)
            { combustivel = combustivel - 2000; }
        }
    }
}
