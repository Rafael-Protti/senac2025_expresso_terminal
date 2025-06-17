/* VARIÁVEIS NUMÉRICAS */

//uso para números inteiros
//int vida = 3;
//int pontos = 0;

//uso para números reais
//float velocidades = 22.5f; // mais leve, limite definido; o Float do C# Exige um "f" no final do número
//double dano = 11.1; // mais pesado, quase infinito

// "//" comentário em linha
// "/**/" comentário em bloco

/* VARIÁVEIS DE TEXTO */

//string nome;
//char letra;

/* Crie uma variável chamada "título" com o nome do seu jogo*/

//string titulo = "Jogo";
//Console.WriteLine(titulo);

/* VARIÁVEIS BOLEANO OU LÓGICAS */

//bool playing = true; */

//SWITCH
/*string tecla;
            do {
                tecla = Console.ReadLine();
                switch (tecla)
                {
                    case "a":
                        Console.WriteLine("Para a esquerda");
                        break;
                    case "w":
                        Console.WriteLine("Para frente");
                        break;
                    case "s":
                        Console.WriteLine("Para trás");
                        break;
                    case "d":
                        Console.WriteLine("Para a direita");
                        break;
                }
            } while (tecla != "x");*/

/* EXPRESSO TERMINAL: O JOGO */

/**Descrição do jogo: Jogador controla uma locomotiva caregando um vargão de carga. O objetico é chegar na Estação sem perder nenhum ponto de carga.
O jogador pode trocar de trilhos conforme sua vontade, tendo as escolhas DIREITA e ESQUERDA ou MANTER TRILHO e TROCAR DE TRILHO. Cada caminho tem obstáculos diferentes e ALEATÓRIOS.
O jogo tem dois tipos de obstáculos, as DESCIDAS devem ser percorridas em velocidade baixa e as SUBIDAS devem ser percorridos em alta velocidade.
Se a locomotiva não estiver dentro do limite da velocidade em cada obstáculo, ela perdera pontos de sua carga.
Também, se a locomotiva estiver muito abaixo ou acima do limite de velocidade requesitados pelos obstáculos, a locomotiva perderá combustível.
Se o jogador não escolher o caminho a ser seguido a tempo, perderá combustível. 
Jogador deve chegar na estação com a maior quantia de carga e o maior depósito de combustível possíveis.

 
 */

namespace Projetto1
{
    class Program
    {
        public static void MostrarVariaveis()
        {
            //variáveis numéricas do jogo
            float velocidade = 0; //MIN=0; MAX=100; Aumenta e Freia com as Setinhas
            int carga = 10; //perde de passar com a velocidade errada nos obstáculos. Se = 0, fim de jogo (derrota).
            float combustivel = 100; //perde se passar muito acima ou muito abaixo da velocidade requerida pelo obstáculo. Consumido com o tempo. Se = 0, fim de jogo (derrota).
            float distancia = 10; //indica quantos Km/m faltam para chegar na Estação. Ganha de = 0

            //variáveis lógicas/bolenas do jogo
            bool direita = false; // Se verdadeiro, a locomotiva está na direita. Se falso, a locomotiva está na esquerda.

            //mensagens para o jogador
            Console.WriteLine("Deseja seguir pelo trilho da esquerda ou da direita?"); //exibe no começo do jogo
            Console.WriteLine("Deseja trocar de trilho?"); //exibe nos próximos cruzamentos
            Console.WriteLine("Parabéns! Você entregou a carga em segurança!"); //exibe se distancia = 0
            Console.WriteLine($"Oops! Você ficou sem combustível. Faltou {distancia}Km para chegar na Estação. FIM DE JOGO."); //exibe se combustivel = 0
            Console.WriteLine($"Essa não! Você derrubou toda a carga! Faltou {distancia}Km para chegar na Estação. FIM DE JOGO"); //exibe se carga = 0


            Console.WriteLine("Deseja seguir pelo trilho da esquerda ou da direita?"); //exibe no começo do jogo

        }
    }
}

