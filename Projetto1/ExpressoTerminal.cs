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
        static int largura = 201; //largura (X) do mapa
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
        static int locomocao;
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

                if (nivel < 4)
                {
                    var tecla = Console.ReadKey(true).Key;
                    AtualizarPosicao(tecla);
                }

                if (playerX >= 175)
                {
                    nivel = nivel + 1;
                    if (nivel < 4)
                    {
                        playerX = 1;
                        Jogar();
                    }
                }

                if (nivel > 3 || combustivel == 0 || carga == 0)
                {
                    jogando = false;
                }
            }
            Console.ReadKey();
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

            if (nivel < 3)
            {
                for (int y = 1; y < altura - 1; y++)
                {
                    mapa[189, y] = '-';
                    mapa[190, y] = '>';
                }
            }

            for (int x = 1; x < largura - 1; x++) //desenha o trilho de cima
            {
                mapa[x, 5] = 'I';
            }

            for (int x = 1; x < largura - 1; x++) //desenha o trilho de baixo
            {
                mapa[x, 10] = 'I';
            }

            if (nivel == 1)
            {
                mapa[50, 1] = '1';
            }

            if (nivel == 2)
            {
                mapa[50, 1] = '2';
            }

            else
            {

                for (int y = 2; y < altura - 1; y++)
                {
                    mapa[187, y] = '|';
                    mapa[199, y] = '|';
                }
                
                for (int x = 188; x < largura - 2; x++)
                {
                    mapa[x, 1] = '-';
                    mapa[x, 14] = '_';
                }

                mapa[50, 1] = '3';
                mapa[190, 1] = 'E';
                mapa[191, 1] = 'S';
                mapa[192, 1] = 'T';
                mapa[193, 1] = 'A';
                mapa[194, 1] = 'Ç';
                mapa[195, 1] = 'Ã';
                mapa[196, 1] = 'O';
                mapa[199, 1] = '.';
                mapa[187, 1] = '.';
                mapa[187, 5] = 'I';
                mapa[187, 10] = 'I';
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
                    if (velocidade > 0) { velocidade = velocidade - 5; };
                    break;
                case ConsoleKey.D:
                    if (velocidade < 120) { velocidade = velocidade + 5; };
                    break;
                case ConsoleKey.F:
                    MovimentoVelocidade();
                    tempX = tempX + locomocao;
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
                    mapa[playerX + x, 5] = 'I';
                }

                for (int x = 0; x < tremX; x++) //Redesenha o trilho no y = 10
                {
                    mapa[playerX + x, 10] = 'I';
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
            bool antisaida = true; //criado para evitar que a tela saia antes de selecionar a tecla correta.
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

                    Aperte L para sair.
                    """);
            do
            {
                var tecla2 = Console.ReadKey(true);
                switch (tecla2.Key)
                {
                    case ConsoleKey.L:
                        antisaida = false;
                        ValoresPadrao();
                        Main();
                        break;
                }
            } while (antisaida);

        }
        static void ValoresPadrao()
        {
            carga = 10;
            combustivel = 100;
            velocidade = 0;
            playerX = 1;
            playerY = 2;
            embaixo = false;
            nivel = 3;
        }

        static void MovimentoVelocidade()
        {
            if (velocidade >= 5 && velocidade <= 25)
            {
                locomocao = 1; //andar 1 espaço
            }
            if (velocidade >= 26 &&  velocidade <= 55)
            {
                locomocao = 2; //andar 2 espaços
            }
            if (velocidade >= 56 && velocidade <= 75)
            {
                locomocao = 3; //andar 3 espaços
            }
            if (velocidade >= 76 && velocidade <= 95)
            {
                locomocao = 4; //andar 4 espaços
            }
            if (velocidade >= 96 && velocidade <= 120)
            {
                locomocao = 5; //andar 5 espaços
            }
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
        
