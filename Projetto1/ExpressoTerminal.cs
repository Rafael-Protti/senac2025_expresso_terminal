using System;
using System.ComponentModel.Design;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Timers;

namespace Projetto1
{
    class ExpressoTerminal
    {
        public static System.Timers.Timer aTimer;
        static char[,] mapa; //variável CHAR que é usada para desenhar o mapa
        static int largura = 200; //largura (X) do mapa
        static int altura = 16; //altura (Y) do mapa
        static int playerX = 1; //posição (X) inicial do jogador
        static int playerY = 2; //posição (Y) inicial do jogador
        static bool rodando = true; //dita se o jogo está rodando ou não. Já vem como TRUE, se FALSO, o jogo fecha
        static bool jogando = false;
        static string trem = "        ____ __   _______ |[]|-||_  |_____|-|_____(_)  o=o=o  00=OO=o/\\o"; //desenho da locomotiva (os dois cotra-barras amarelos servem para desenhar um só e contam como UM caractér)
        static int tremX = 18; //largura (X) da locomotiva
        static int tremY = 4; //largura (Y) da locomotiva
        static int velocidade = 0;
        static bool embaixo; // Se verdadeiro, a locomotiva no trilho de cima. Se falso, a locomotiva está no trilho de baixo.
        static int nivel; //dita qual layout de qual nível vai carregar (1 -> nível 1, 2 -> nível 2, 3 -> nível 3
        static int carga; //perde de passar com a velocidade errada nos obstáculos. Se = 0, fim de jogo (derrota).
        static float combustivel; //perde se passar muito acima ou muito abaixo da velocidade requerida pelo obstáculo. Consumido com o tempo. Se = 0, fim de jogo (derrota).
        static void Main()
        {
            Console.Clear();

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("._________________.\n|EXPRESSO TERMINAL|\nº-----------------º");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n-> Jogar     [j]\n-> Créditos  [k]\n-> Sair      [l]");
                Console.ResetColor();
                Console.WriteLine("\nDigite uma tecla para continuar:");

                var botao = Console.ReadKey(true);
                switch (botao.Key)
                {
                    case ConsoleKey.J:
                        ValoresPadrao();
                        jogando = true;
                        Jogar();
                        break;
                    case ConsoleKey.K:
                        Creditos();
                        break;
                    case ConsoleKey.L:
                        rodando = false;
                        break;
                }
            } while (rodando); Console.Clear(); Console.WriteLine("Obrigado por jogar!");
        }

        static void Creditos()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Instrutor: Marcius \nCriador: Rafael Protti");
            Console.ResetColor();
            Console.WriteLine("\nAperte qualquer tecla para voltar.");
            Console.ReadKey();
        }

        static void Jogar()
        {
            IniciarMapa();
            while (jogando)
            {
                Console.Clear();
                DesenharMapa();

                Console.WriteLine($"""
                    Velocidade: {velocidade}
                    Carga: {carga}
                    Combustível: {combustivel}
                    Distância: {playerX}
                    Nível: {nivel}
                    """);

                if (playerX == 20)
                {
                    nivel = nivel + 1;
                    if (nivel < 4)
                    {
                        playerX = 1;
                        Jogar();
                    }
                }

                if (nivel > 3)
                {
                    jogando = false;
                }

                if (combustivel == 0 || carga == 0)
                {
                    jogando = false;
                }

                var tecla = Console.ReadKey(true).Key;
                AtualizarPosicao(tecla);
            }
            TelaFimJogo();

        }

        static void IniciarMapa()
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

            for (int x = 1; x < largura - 1; x++) //desenha o trilho de cima
            {
                mapa[x, 5] = '|';
            }

            for (int x = 1; x < largura - 1; x++) //desenha o trilho de baixo
            {
                mapa[x, 10] = '|';
            }

            if (nivel == 1)
            {
                mapa[50, 1] = '1';
            }

            else if (nivel == 2)
            {
                mapa[50, 1] = '2';
            }

            else
            {
                mapa[50, 1] = '3';
            }


                for (int y = 0; y < tremY; y++) //desenha a locomotiva
                {
                    for (int x = 0; x < tremX; x++)
                    {
                        mapa[playerX + x, playerY + y] = trem[y * tremX + x];
                    }
                    Console.WriteLine();
                }
        }
        static void DesenharMapa()
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
        static void AtualizarPosicao(ConsoleKey tecla)
        {

            int tempX = playerX;
            int tempY = playerY;
            

            switch (tecla)
            {
                case ConsoleKey.A:
                    velocidade--;
                    tempX--;
                    break;
                case ConsoleKey.D:
                    velocidade++;
                    tempX++;
                    break;
                case ConsoleKey.W:
                    embaixo = false;
                    //tempY--;
                    break;
                case ConsoleKey.S:
                    embaixo = true;
                    //tempY++;
                    break;
                case ConsoleKey.L:
                    ValoresPadrao();
                    Main();
                    break;
                case ConsoleKey.M:
                    combustivel--;
                    break;
                case ConsoleKey.N:
                    carga--;
                    break;
            }

            if (embaixo) //posiciona a locomotiva no trilho de cima e no trilho de baixo.
            {
                tempY = 7;
            } else { tempY = 2; }

            if (mapa[tempX, tempY] != '#' && mapa[tempX + tremX, tempY + tremY] != '#')
            {
                for (int y = 0; y < tremY; y++)
                {
                    for (int x = 0; x < tremX; x++)
                    {
                        mapa[playerX + x, playerY + y] = ' ';
                    }
                }

                for (int x = 0; x < tremX; x++) //Redesenha o trilho no Y = 5
                {
                    mapa[playerX + x, 5] = '|';
                }

                for (int x = 0; x < tremX; x++) //Redesenha o trilho no y = 10
                {
                    mapa[playerX + x, 10] = '|';
                }

                for (int y = 0; y < tremY; y++)
                {
                    for (int x = 0; x < tremX; x++)
                    {
                        mapa[tempX + x, tempY + y] = trem[y * tremX + x];
                    }
                }

                playerX = tempX;
                playerY = tempY;
            }

        }

        static void TelaFimJogo()
        {
            Console.Clear();
            if (nivel > 3)
            {
                Console.WriteLine("Parabéns, você venceu!!! Fim de jogo!");
            }
            else 
            { Console.WriteLine("Você perdeu!!! Fim de jogo!"); }
                Console.WriteLine($"""
                    Mercadoria entregue: {carga} Ton
                    Combustível restante: {combustivel} Kg
                    Distância percorrida {playerX} Km
                    """);
            var botao = Console.ReadKey(true);
            switch(botao.Key)
            {
                case(ConsoleKey.L):
                    ValoresPadrao();
                    Main();
                    break;
            }
            
        }
        static void ValoresPadrao()
        {
            carga = 10;
            combustivel = 100;
            velocidade = 0;
            playerX = 1;
            playerY = 2;
            embaixo = false;
            nivel = 1;
        }
        
        static void Temporizador()
        {
            aTimer = new System.Timers.Timer(2000);
            aTimer.Elapsed += MovimentoHorizontal;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        static void MovimentoHorizontal(Object source,ElapsedEventArgs e)
        {
            playerX++;
        }
        
    }
}
        
