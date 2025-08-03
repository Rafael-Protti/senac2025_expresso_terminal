using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
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
        public Vector2 pos = new Vector2(0,0);
        public Locomotiva locomotiva = new Locomotiva();

        public Nivel nivel = new Nivel();

        public void IniciarMapa()
        {
            Pixel parede = new Pixel("#", ConsoleColor.Red, 1, 1, pos);
            Pixel espaco = new Pixel(" ", ConsoleColor.Black, 1, 1, pos);
            Pixel trilho = new Pixel("I", ConsoleColor.DarkGray, 1, 1, pos);
            Pixel subida_pixel = new Pixel("|", ConsoleColor.Yellow, 1, 1, pos);
            Pixel descida_pixel = new Pixel("i", ConsoleColor.DarkYellow, 1, 1, pos);
            Pixel flecha = new Pixel(">", ConsoleColor.DarkMagenta, 1, 1, pos);
            Obstaculos subida = new Obstaculos(subida_pixel, nivel.fase);
            Obstaculos descida = new Obstaculos(descida_pixel, nivel.fase);
            Pixel trem = new Pixel(locomotiva.tremdesenho, ConsoleColor.Cyan, locomotiva.tremX, locomotiva.tremY, locomotiva.pos);

            mapa = new Pixel[largura, altura];

            for (pos.y = 0; pos.y < altura; pos.y++)
            {
                for (pos.x = 0; pos.x < largura; pos.x++)
                {
                    //ultima posição do vetor é tamanho -1, pois começa no ZERO!!!!!!!!!!!!!!!!!!!!!!!!!!
                    if (pos.x == 0 || pos.y == 0 || pos.x == largura - 1 || pos.y == altura - 1)
                    {
                        mapa[pos.x, pos.y] = parede;
                    }
                    else if (pos.y == 5 || pos.y == 10)
                    {
                        mapa[pos.x, pos.y] = trilho;
                    }
                    else if (pos.x == 165)
                    {
                        mapa[pos.x, pos.y] = flecha;
                    }
                    else if (pos.x == 1)
                    {
                        mapa[pos.x, pos.y] = trem;
                    }
                    else
                    {
                        mapa[pos.x, pos.y] = espaco;
                    }
                    mapa[1, 3] = espaco; mapa[1, 4] = espaco; mapa[1, 8] = espaco; mapa[1, 9] = espaco;
                }
            }
            for (int z = 0; z < nivel.fase; z++)
            {
                do { subida.Randomizer(); descida.Randomizer(); } while (subida.pos.y == descida.pos.y || subida.pos.x == descida.pos.y);
                for (pos.x = subida.pos.x; pos.x < subida.distancia; pos.x++)
                {
                    mapa[pos.x, subida.pos.y] = subida.forma;
                }
                for (pos.x = descida.pos.x; pos.x < descida.distancia; pos.x++)
                {
                    mapa[pos.x, descida.pos.y] = descida.forma;
                }
            }
        }
        public override void Draw()
        {
            DesenharMapa();
            Interface();
            //if (locomotiva.visible) { locomotiva.Draw(); }
        }

        private void DesenharMapa()
        {
            for (pos.y = 0; pos.y < altura; pos.y++)
            {
                for (pos.x = 0; pos.x < largura; pos.x++)
                {
                    mapa[pos.x,pos.y].Show();
                }
            }
        }

        private void Interface()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write($"""

                Velocidade ( km/h ): {locomotiva.velocidade} 
                Combustível ( % ): {locomotiva.combporcento} 
                Carga ( ton ): {locomotiva.carga} 
                Distância ( km ): {locomotiva.distancia} 

                Nível : {nivel.fase}
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
