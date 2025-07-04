using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetto1
{
    internal class GameManager
    {
        private GameManager() { }
        private static GameManager instancia;
        static public GameManager Instancia => instancia ??= new GameManager();

    }
    
}
