using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetto1
{
    public class Mapa : MonoBehaviour
    {
        private Mapa() {
            Run();
        }

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
        private void DesenharMapa()
        {
            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                    Console.Write(mapa[x, y]);
                }
                Console.WriteLine();
            }

        }

        public override void Update()
        {
            DesenharMapa(); 
        }

        public override void Start()
        {
            IniciarMapa();
        }
    }

}
