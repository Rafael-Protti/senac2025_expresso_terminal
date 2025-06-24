using System;
using System.ComponentModel.Design;
using System.Runtime;
using System.Runtime.ExceptionServices;

namespace Projetto1
{
    class ExpressoTerminal
    {
        static char[,] mapa; //variável CHAR que é usada para desenhar o mapa
        static int largura = 200; //largura (X) do mapa
        static int altura = 16; //altura (Y) do mapa
        static int playerX = 1; //posição (X) inicial do jogador
        static int playerY = 2; //posição (Y) inicial do jogador
        static bool rodando = true; //dita se o jogo está rodando ou não. Já vem como TRUE, se FALSO, o jogo fecha
        static string trem = "        ____ __   _______ |[]|-||_  |_____|-|_____(_)  o=o=o  00=OO=o/\\o"; //desenho da locomotiva (os dois cotra-barras amarelos servem para desenhar um só e contam como UM caractér)
        static int tremX = 18; //largura (X) da locomotiva
        static int tremY = 4; //largura (Y) da locomotiva
        static bool embaixo = false; // Se verdadeiro, a locomotiva no trilho de cima. Se falso, a locomotiva está no trilho de baixo.
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
            //variáveis numéricas do jogo
            float velocidade = 0; //MIN=0; MAX=100; Aumenta e Freia com as Setinhas
            int carga = 10; //perde de passar com a velocidade errada nos obstáculos. Se = 0, fim de jogo (derrota).
            float combustivel = 100; //perde se passar muito acima ou muito abaixo da velocidade requerida pelo obstáculo. Consumido com o tempo. Se = 0, fim de jogo (derrota).

            IniciarMapa();
            while (rodando)
            {
                Console.Clear();
                DesenharMapa();

                Console.WriteLine($"""
                    Velocidade: {velocidade}
                    Carga: {carga}
                    Combustível: {combustivel}
                    Distância: {playerX}
                    """);
                if (playerX == 180)
                {
                    embaixo = false;
                    playerX = 1;
                    Main();
                }

                var tecla = Console.ReadKey(true).Key;
                AtualizarPosicao(tecla);

            }
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

            for(int x = 1; x < largura - 1; x++)
            {
                mapa[x, 5] = '|';
            }

            for (int x = 1; x < largura - 1; x++)
            {
                mapa[x, 10] = '|';
            }


            for (int y = 0; y < tremY; y++)
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
                    tempX--;
                    break;
                case ConsoleKey.D:
                    tempX++;
                    break;
                case ConsoleKey.W:
                    embaixo = false;
                    break;
                case ConsoleKey.S:
                    embaixo = true;
                    break;
                case ConsoleKey.L:
                    playerX = 1;
                    playerY = 2;
                    Main();
                    break;
            }
            if (embaixo == false) //posiciona a locomotiva no trilho de cima e no trilho de baixo.
            {
                tempY = 2;
            }
            else
            {
                tempY = 7;
            }

            if (mapa[tempX, tempY] != '#' && mapa[tempX + tremX, tempY + tremY] != '#')
            {
                for (int y = 0; y < tremY; y++)
                {
                    for (int x = 0; x < tremX; x++)
                    {
                        mapa[playerX + x, playerY + y] = ' ';
                    }
                }

                for (int x = 0; x < tremX; x++) //Redesenha o trilho no Y=5
                {
                    mapa[playerX + x, 5] = '|';
                }

                for (int x = 0; x < tremX; x++) //Redesenha o trilho no y=10
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
    }
}
        
