using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetto1
{
    internal class Obstaculos
    {
        public int x { get; set; }
        public int y { get; set; }
        public string forma { get; set; }
    }

    class Subida : Obstaculos
    {
        public Subida()
        {
            forma = "||||||||||";
        }


        public void Consequencia()
        {
            
        }
    }

    class Descida : Obstaculos
    {
        public Descida()
        {
            forma = "iiiiiiiiii";
        }
        public void Consequencia()
        {

        }
    }
}
