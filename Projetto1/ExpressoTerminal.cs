using System;
using System.ComponentModel.Design;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Timers;

//HOJE: Transformar movimentação em CLASSE
//Mover a movimentação atual para a Classe Personagem
//Criar os obstáculos e suas consequências (Usar Orientação voltada a Objeto e Polimorfismo)
//Movimentação automática (esperar aula de Thread)
//Criar obstaculos aleatoriamente no mapa


namespace Projetto1
{
    class ExpressoTerminal
    {
        public static System.Timers.Timer aTimer;
        static char[,] mapa; //variável CHAR que é usada para desenhar o mapa
        static int largura = 185; //largura (X) do mapa
        static int altura = 16; //altura (Y) do mapa
        static int playerX = 1; //posição (X) inicial do jogador
        static int playerY = 2; //posição (Y) inicial do jogador
        static bool rodando = true; //dita se o jogo está rodando ou não. Já vem como TRUE, se FALSO, o jogo fecha
        static bool jogando = false; //dita se a gameplay está rodando ou não.
        static string trem = "        ____ __   _______ |[]|-||_  |_____|-|_____(_)  o=o=o  00=OO=o/\\o"; //desenho da locomotiva (os dois con tra-barras amarelos servem para desenhar um só e contam como UM caractér)
        static int tremX = 18; //largura (X) da locomotiva
        static int tremY = 4; //largura (Y) da locomotiva
        static int velocidade = 0;
        static bool embaixo; // Se verdadeiro, a locomotiva no trilho de cima. Se falso, a locomotiva está no trilho de baixo.
        static int nivel; //dita qual layout de qual nível vai carregar (1 -> nível 1, 2 -> nível 2, 3 -> nível 3
        static int carga; //perde de passar com a velocidade errada nos obstáculos. Se = 0, fim de jogo (derrota).
        static float combustivel; //perde se passar muito acima ou muito abaixo da velocidade requerida pelo obstáculo. Consumido com o tempo. Se = 0, fim de jogo (derrota).
        static int locomocao; //quantos espaços a locomotiva vai andar. 
        static int percorrido = 0; //quantos Xs foram andados (usado para a exibição da distância percorrida)
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
                int distancia = percorrido + playerX;
                Console.Clear();
                DesenharMapa();
                Console.WriteLine($"""
                    Velocidade:{velocidade}
                    Carga:{carga}
                    Combustível:{combustivel}
                    Distância:{distancia}
                    Nível:{nivel}
                    """);

                if (nivel < 5)
                {
                    var tecla = Console.ReadKey(true).Key;
                    AtualizarPosicao(tecla);
                }

                if (playerX >= 160 && nivel < 4)
                {
                    percorrido += playerX;
                    nivel = nivel + 1;
                    if (nivel < 5)
                    {
                        playerX = 1;
                        Jogar();
                    }
                }

                if (nivel > 4 || combustivel <= 0 || carga <= 0 || nivel == 4 && playerX >= 160)
                {
                    jogando = false;
                }
            }
            Console.ReadKey();
            if (jogando == false && rodando == true) { TelaFimJogo(); }

        }

        static void IniciarMapa()
        {
            mapa = new char[largura, altura];
            char descida = 'i';
            int descidaX;
            int descidaY;
            char subida = '|';
            List<int> subidaX;
            int subidaY;
            
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

            if (nivel < 4)
            {
                for (int y = 1; y < altura - 1; y++)
                {
                    mapa[159, y] = '-';
                    mapa[160, y] = '>';
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
                for (int x = 40; x < 51; x++)
                {
                    mapa[x, 5] = descida;
                }
            }

            if (nivel == 2)
            {
                mapa[50, 1] = '2';
            }

            if (nivel == 3)
            {
                mapa[50, 1] = '3';
            }

            if (nivel == 4)
            {

                for (int y = 2; y < altura - 1; y++)
                {
                    mapa[160, y] = '|';
                    mapa[182, y] = '|';
                }
                
                for (int x = 161; x < largura - 3; x++)
                {
                    mapa[x, 1] = '-';
                    mapa[x, 14] = '_';
                }

                mapa[50, 1] = '4';
                mapa[168, 1] = 'E';
                mapa[169, 1] = 'S';
                mapa[170, 1] = 'T';
                mapa[171, 1] = 'A';
                mapa[172, 1] = 'Ç';
                mapa[173, 1] = 'Ã';
                mapa[174, 1] = 'O';
                mapa[182, 1] = '.';
                mapa[160, 1] = '.';
                mapa[160, 5] = 'I';
                mapa[160, 10] = 'I';
                mapa[182, 5] = 'I';
                mapa[182, 10] = 'I';
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
                    tempX +=locomocao;
                    GastoCombustivel();
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
                    jogando = false;
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
            if (nivel > 5)
            {
                Console.WriteLine("Parabéns, você venceu!!! Fim de jogo!");
            }
            else 
            { Console.WriteLine("Você perdeu!!! Fim de jogo!"); }

            Console.WriteLine($"""
                    Mercadoria entregue: {carga} To
                    Combustível restante: {combustivel} Kg
                    Distância percorrida {percorrido + playerX} Km

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
            combustivel = 10000;
            velocidade = 0;
            playerX = 1;
            playerY = 2;
            embaixo = false;
            nivel = 1;
            percorrido = 0;
        }

        static void MovimentoVelocidade()
        {
            if (velocidade == 0) { locomocao = 0; }
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

        static void GastoCombustivel()
        {
            combustivel -= locomocao*10;
            if (playerX <= 50 && playerX >= 40 && playerY == 2 && velocidade > 30)
            { combustivel = combustivel - 2000; }
        }
    }
}
        
