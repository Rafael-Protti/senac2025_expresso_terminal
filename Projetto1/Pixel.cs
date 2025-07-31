using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetto1
{
    public class Pixel
    {
        public string icone;
        public ConsoleColor cor;

        public Pixel(string icone, ConsoleColor cor)
        {
            this.icone = icone;
            this.cor = cor;
        }
        public void Show()
        {
            Console.ForegroundColor = cor;
            Console.Write(icone);
            Console.ResetColor();
        }
    }
}
