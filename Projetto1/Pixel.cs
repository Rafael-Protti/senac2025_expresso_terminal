using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Projetto1
{
    public class Pixel
    {
        public string icone;
        public ConsoleColor cor;
        public int largura;
        public int altura;
        public Vector2 pos;

        public Pixel(string icone, ConsoleColor cor, int largura, int altura, Vector2 pos)
        {
            this.icone = icone;
            this.cor = cor;
            this.largura = largura;
            this.altura = altura;
            this.pos = pos;
        }
        public void Show()
        {
            for (int y = 0; y < altura; y++) //desenha a locomotiva
            {
                for (int x = 0; x < largura; x++)
                {
                    Console.ForegroundColor = cor;
                    Console.SetCursorPosition(pos.x + x, pos.y + y);
                    Console.Write(icone[y * largura + x]);
                    Console.ResetColor();
                }
            }
            
        }
    }
}
