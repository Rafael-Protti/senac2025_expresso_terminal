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

        public char[,] mapa; //variável CHAR que é usada para desenhar o mapa
        public int largura = 185; //largura (X) do mapa
        public int altura = 16; //altura (Y) do mapa

        private void IniciarMapa()
        {
            mapa = new char[largura, altura];

            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                    //ultima posição do vetor é tamanho -1, pois começa no ZERO!!!!!!!!!!!!!!!!!!!!!!!!!!
                    if (x == 0 || y == 0 || x == largura - 1 || y == altura - 1)
                    {
                        mapa[x, y] = '#';
                    }
                    else
                    {
                        mapa[x, y] = ' ';
                    }
                }
            }
        }
        public override void Draw()
        {
            GameManager GM2 = GameManager.Instancia;
            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                    Console.Write(mapa[x, y]);
                }
                Console.WriteLine();
            }

            for (int x = 1; x < largura - 1; x++) //desenha o trilho de cima
            {
                mapa[x, 5] = 'I';
            }

            for (int x = 1; x < largura - 1; x++) //desenha o trilho de baixo
            {
                mapa[x, 10] = 'I';
            }



            Console.Write("Velocidade: " + GM2.trem.velocidade);

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
