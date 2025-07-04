using System;

namespace Projetto1
{
    class ExpressoTerminalBackup
    {
        static void Backup()
        {
            bool rodando = true;
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
                        Jogando();
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
            Console.WriteLine("\nAperte Enter para voltar.");
            Console.ReadLine();

        }
        static void Jogando()
        {
            Console.Clear ();
            //variáveis numéricas do jogo
            float velocidade = 0; //MIN=0; MAX=100; Aumenta e Freia com as Setinhas
            int carga = 10; //perde de passar com a velocidade errada nos obstáculos. Se = 0, fim de jogo (derrota).
            float combustivel = 100; //perde se passar muito acima ou muito abaixo da velocidade requerida pelo obstáculo. Consumido com o tempo. Se = 0, fim de jogo (derrota).
            float distancia = 10; //indica quantos Km/m faltam para chegar na Estação. Ganha se = 0

            //variáveis lógicas/bolenas do jogo
            bool direita = false; // Se verdadeiro, a locomotiva está na direita. Se falso, a locomotiva está na esquerda.

            //mensagens para o jogador
            Console.ForegroundColor = ConsoleColor.White;
            //Console.WriteLine("Deseja seguir pelo trilho da esquerda ou da direita?"); //exibe no começo do jogo
            Console.WriteLine("Deseja trocar de trilho?"); //exibe nos próximos cruzamentos
            Console.WriteLine("Parabéns! Você entregou a carga em segurança!"); //exibe se distancia = 0
            Console.WriteLine($"Oops! Você ficou sem combustível. Faltou {distancia}Km para chegar na Estação. FIM DE JOGO."); //exibe se combustivel = 0
            Console.WriteLine($"Essa não! Você derrubou toda a carga! Faltou {distancia}Km para chegar na Estação. FIM DE JOGO"); //exibe se carga = 0
            Console.ResetColor();

            //versão diálogos
            string botao; 
            Console.WriteLine("Deseja seguir pelo trilho da esquerda ou da direita?\nEsquerda[A]\nDireita [D]");
            botao = Console.ReadLine().ToLower();
            if (botao == "d")
            {
                Console.Clear();
                Console.WriteLine("Você escolheu o trilho da direita!");
            }
            else if (botao == "a")
            {
                Console.Clear();
                Console.WriteLine("Você escolheu o trilho da esquerda!");
            }

           
            Console.WriteLine("\nAperte Enter para voltar.");
            Console.ReadLine();

  


        }
    }
}


/*Singleton
	player
	hud
	mapa
	menu
	jogo (GameManager)
---
class GameManager
{
	private GameManager() {}
	private GameManager instancia;
	*static public GameManager Instancia => instancia??=new GameManager();
	public Player p1;
	public HUD hdu;
	public Mapa mapa;

}

class Player
{
	private Player() {}
	private Player instancia;
	static public Player Instancia => instancia??=new Player();
	
	public list<Item> iventario;
	private stirng nome

}

public void Jogo()
{
	GameManager.Instancia.p1.Inventario.Ad(...)
	ou
	GameManager g = GameManager.Instancia;
	Player p=g.p1
	p.Inventario.Add(new Item());
}
--
	static//acessa pela classe// public//acessa de fora// GameManager//retorna a classe// Instancia()//nome da função
--
{
	*if (instancia != null)
	{
		return instancia;
	}
	instancia = new GameManager();
	return instancia;
}
 */
