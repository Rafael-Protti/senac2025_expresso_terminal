using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Projetto1
{
    public class Mapa : MonoBehaviour
    {
        private Mapa() { Run(); }

        private static Mapa instancia;
        static public Mapa Instancia => instancia ??= new Mapa();

        public Pixel[,] mapa; //variável CHAR que é usada para desenhar o mapa
        public int largura = 185; //largura (X) do mapa
        public int altura = 16; //altura (Y) do mapa
        public static Vector2 pos = new Vector2(0, 0);
        public Locomotiva locomotiva = new Locomotiva();
        public static Nivel nivel = new Nivel();
        public Pixel parede = new Pixel("#", ConsoleColor.Red, 1, 1, pos);
        public Pixel espaco = new Pixel(" ", ConsoleColor.Black, 1, 1, pos);
        public Pixel trilho = new Pixel("|", ConsoleColor.DarkGray, 1, 1, pos);
        public static Pixel subida_pixel = new Pixel("I", ConsoleColor.Magenta, 1, 1, pos);
        public static Pixel descida_pixel = new Pixel("i", ConsoleColor.Yellow, 1, 1, pos);
        public static Pixel trilhoq_pixel = new Pixel("H", ConsoleColor.Red, 1, 1, pos);
        public Pixel flecha = new Pixel(">", ConsoleColor.Gray, 1, 1, pos);
        public Obstaculos subida = new Obstaculos(subida_pixel, nivel.dificuldade);
        public Obstaculos descida = new Obstaculos(descida_pixel, nivel.dificuldade);
        public Obstaculos trilhoq = new Obstaculos(trilhoq_pixel, nivel.dificuldade);

        public void IniciarMapa()
        {
            Pixel trem = new Pixel(locomotiva.tremdesenho, ConsoleColor.Cyan, locomotiva.tremX, locomotiva.tremY, locomotiva.pos);
            descida.LimparListas(); subida.LimparListas(); trilhoq.LimparListas();
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
            for (int z = 0; z < nivel.dificuldade; z++)
            {
                subida.Randomizer(); descida.Randomizer(); trilhoq.Randomizer();
                do
                {
                    subida.LimparUltimo(); descida.LimparUltimo(); trilhoq.LimparUltimo();
                    subida.Randomizer(); descida.Randomizer(); trilhoq.Randomizer();
                } while (subida.pos == descida.pos || subida.pos == trilhoq.pos || descida.pos == trilhoq.pos);

                mapa[subida.pos.x, subida.pos.y] = subida.forma;
                mapa[descida.pos.x, descida.pos.y] = descida.forma;
                mapa[trilhoq.pos.x, trilhoq.pos.y] = trilhoq.forma;
            }
        }
        public override void Draw()
        {
            DesenharMapa();
            Interface();
        }

        private void DesenharMapa()
        {
            for (pos.y = 0; pos.y < altura; pos.y++)
            {
                for (pos.x = 0; pos.x < largura; pos.x++)
                {
                    mapa[pos.x, pos.y].Show();
                }
            }
        }

        private void Interface()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine($"Velocidade ( km/h ): {locomotiva.velocidade} ");
            Console.WriteLine($"Combustível ( % ): {locomotiva.combporcento} ");
            Console.WriteLine($"Carga ( ton ): {locomotiva.recursos.carga} ");
            Console.WriteLine($"Distância ( km ): {locomotiva.distancia} ");
            Console.WriteLine();
            Console.WriteLine($"Nível : {nivel.fase}");
            Console.WriteLine();
            Console.WriteLine("""
            Aperte D para acelerar e A para freiar. A locomotiva perde combustível conforme anda.
            W e S mudam a locomotiva de trilho em troca de combustível.
            Passe em velocidade baixa (20-40 km/k) nas descidas ( i ) para não derrubar carga.
            Passe em velocidade alta (80-100 km/h) nas subidas ( I ) para não perder combustível.
            Não cruze os trilhos quebrados ( H ), pois você pode perder todos os seus recursos de uma vez.
            Chegue no final do nível 3 para chegar na estação e ganhar o jogo.
            """);
            Console.ResetColor();
        }

        public void RedesenharMapa() //Classe que redesenha o mapa após a conclusão de um nível.
        {

            if (locomotiva.pos.x >= 165)
            {
                locomotiva.percorrido += locomotiva.pos.x;
                locomotiva.pos.x = 1;
                nivel.ProxNivel();
                IniciarMapa();
                locomotiva.velocidade = 0;
            }
        }
        public override void Awake()
        {
            locomotiva.visible = true;
            locomotiva.input = true;
        }

        public override void Update()
        {
            
        }

        public override void LateUpdate()
        {
            if (locomotiva.input == true) { locomotiva.Movimento(); } //movimento automático da locomotiva.
            if (locomotiva.velocidade > 40) { locomotiva.recursos.Perda(locomotiva.pos, descida.registrox, descida.registroy, 0, 1); }
            if (locomotiva.velocidade < 80) { locomotiva.recursos.Perda(locomotiva.pos, subida.registrox, subida.registroy, 200, 0); }
            locomotiva.recursos.Perda(locomotiva.pos, trilhoq.registrox, trilhoq.registroy, 200, 1);
        }

        public override void Start()
        {
            IniciarMapa();
        }

        public override void OnDestroy()
        {
            locomotiva.input = false;
            locomotiva.visible = false;
            visible = false;
            Console.Clear();
        }
    }

}
