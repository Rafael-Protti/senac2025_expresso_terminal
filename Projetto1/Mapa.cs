using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetto1
{
    public class Mapa : MonoBehaviour
    {
        private Mapa() {Run();}

        private static Mapa instancia;
        static public Mapa Instancia => instancia ??= new Mapa();

        public Pixel[,] mapa; //variável CHAR que é usada para desenhar o mapa
        public int largura = 185; //largura (X) do mapa
        public int altura = 16; //altura (Y) do mapa
        public Pixel parede = new Pixel('#',ConsoleColor.Red);
        public Pixel espaco = new Pixel(' ',ConsoleColor.Black);
        public Pixel trilho = new Pixel('I',ConsoleColor.DarkGray);

        private void IniciarMapa()
        {
            mapa = new Pixel[largura, altura];

            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                    //ultima posição do vetor é tamanho -1, pois começa no ZERO!!!!!!!!!!!!!!!!!!!!!!!!!!
                    if (x == 0 || y == 0 || x == largura - 1 || y == altura - 1)
                    {
                        mapa[x, y] = parede;
                    }
                    else if (y == 5 || y == 10)
                    {
                        mapa[x, y] = trilho;
                    }
                    else
                    {
                        mapa[x, y] = espaco;
                    }
                }
            }
        }
        public override void Draw()
        {
            DesenharMapa();
            Interface();
        }

        private void DesenharMapa()
        {
            
            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                    mapa[x,y].Show();
                }
                Console.WriteLine();
            }
        }

        private void Interface()
        {
            Console.ForegroundColor = ConsoleColor.White;
            GameManager GM2 = GameManager.Instancia;
            Console.Write($"""

                Velocidade: {GM2.trem.velocidade}
                Combustível: {GM2.trem.combustivel}
                Distância: {GM2.trem.pos.x}
                """);
            Console.ResetColor();
        }

        public override void Update()
        {
            
        }

        public override void Start()
        {
            IniciarMapa();
        }
    }

}
