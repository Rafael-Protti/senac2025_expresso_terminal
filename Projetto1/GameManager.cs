﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetto1
{
    public class GameManager : MonoBehaviour
    {
        private GameManager() 
        {
            Run();
        }
        private static GameManager instancia;
        static public GameManager Instancia => instancia ??= new GameManager(); //manter atributos abaixo do singleton

        public bool jogando = false;
        public Menu menu = Menu.Instancia;
        public Mapa mapa = Mapa.Instancia;


        public override void Awake()
        {
            
        }
        public override void Update() {
            Console.SetCursorPosition(0, 0);
            Draw();
            
        }

        public override void Draw()
        {
            if (menu.visible) { menu.Draw();}
            if (mapa.visible) { mapa.Draw();}
        }

        public override void LateUpdate()
        {
           if (mapa.trem.input == true) { mapa.trem.Movimento();} //movimento automático da locomotiva.
        }
        public override void OnDestroy()
        {
            Console.Clear();
            Console.WriteLine("Obrigado por jogar!");
        }



    }
    
}
