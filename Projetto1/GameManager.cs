using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetto1
{
    internal class GameManager : MonoBehaviour
    {
        private GameManager() { }
        private static GameManager instancia;
        static public GameManager Instancia => instancia ??= new GameManager(); //manter atributos abaixo do singleton

        public bool jogando = false;
        public Locomotiva trem;

        public override void Update() {
            if (jogando) {
                Mapa mapa = Mapa.Instancia;
                trem = new Locomotiva();
            }
            else
            {
                Menu menu = Menu.Instancia;
            }
        }
        public override void OnDestroy()
        {
            Console.Clear();
            Console.WriteLine("Obrigado por jogar!");
        }
    }
    
}
