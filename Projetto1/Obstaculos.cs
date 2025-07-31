using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetto1
{
    public class Obstaculos
    {
        public Pixel forma;
        public Vector2 posicao = new Vector2(1,1);
        public Random random = new Random();
        public Obstaculos(Pixel forma)
        {
            this.forma = forma;
        }

        public void Randomizer()
        {
            posicao.x = random.Next(10,180);
            int y = random.Next(2);
            if (y == 0) { posicao.y = 5; }
            else { posicao.y = 10; }
        }
    }

    
}
