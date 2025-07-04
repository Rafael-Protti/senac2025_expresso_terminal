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

        public int x;
        public int y;
        string trem = "        ____ __   _______ |[]|-||_  |_____|-|_____(_)  o=o=o  00=OO=o/\\o";
        public int tremX = 18;
        public int tremY = 4;
        public int velocidade = 0;
        public bool embaixo = false;
        public int locomocao;

        public Locomotiva(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int Teclas(ConsoleKey tecla)
        {
            switch (tecla)
            {
                case ConsoleKey.R:
                    Movimento();
                    x += locomocao;
                    return x;
                case ConsoleKey.F:
                    TrocarTrilho();
                    break;
                case ConsoleKey.C:
                    DiminuirVelocidade();
                    break;
                case ConsoleKey.V:
                    AumentarVelocidade();
                    break;
            }
            return x;
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
    }
}
