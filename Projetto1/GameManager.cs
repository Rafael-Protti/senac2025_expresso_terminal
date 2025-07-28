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
        private GameManager() 
        {
            Run();
        }
        private static GameManager instancia;
        static public GameManager Instancia => instancia ??= new GameManager(); //manter atributos abaixo do singleton

        public bool jogando = false;
        public Locomotiva trem = new Locomotiva();
        public Menu menu = Menu.Instancia;
        public Mapa mapa = Mapa.Instancia;


        public override void Awake()
        {
            menu.visible = true;
            menu.input = true;
        }
        public override void Update() {
            Draw();
        }

        public override void Draw()
        {
            if (menu.visible) { menu.Draw();}
            if (mapa.visible) { mapa.Draw();}
            if (trem.visible) { trem.Draw();}
        }

        public override void LateUpdate()
        {
        }
        public override void OnDestroy()
        {
            Console.Clear();
            Console.WriteLine("Obrigado por jogar!");
        }



    }
    
}
