using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetto1
{
    public class Vector2
    {
        public int x; //atributo
        public int y; //atributo

        public Vector2(int x, int y) //construtor
        {
            this.x = x;
            this.y = y;
        }

        public int Up => this.y -= 1;
        public int Down => this.y += 1;
        public int Left => this.x -= 1;
        public int Right => this.x += 1;

        public static int Distance(Vector2 a, Vector2 b)
        {
            if (a == null) return -1;
            if (b == null) return -1;
            return (
                (int) Math.Sqrt(
                    (a.x - b.x) * (a.x - b.x) +
                    (a.y - b.y) * (a.y - b.y)
                    )
                );
        }
    }
}
