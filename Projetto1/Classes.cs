using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetto1
{
    class Jogador
    {
        public int x;
        public int y;
        string forma = "        ____ __   _______ |[]|-||_  |_____|-|_____(_)  o=o=o  00=OO=o/\\o";
        public int Movimentar(ConsoleKey tecla)
        {
            switch (tecla)
            {
                case ConsoleKey.F:
                    x += 1;
                    return x;
                case ConsoleKey.W:
                    y = 2; 
                    return y;
                case ConsoleKey.S:
                    y = 7;
                    return y;
            }
            return x;
        }
        public Jogador(int x, int y)
        { 
            this.x = x;
            this.y = y;
        }

        class Obstaculos
        {
            public int x { get; set; }
            public int y { get; set; }
            public string nome { get; set; }
        }

        class Subida:Obstaculos
        {

        }

        class Descida:Obstaculos
        {

        }
    }
}
