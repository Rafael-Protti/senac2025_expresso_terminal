using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Projetto1
{
    public class Nivel
    {
        public int fase;
        public int dificuldade;
        public int nivelmax;
        public Nivel()
        {
            fase = 1;
            dificuldade = 1;
        }

        public void ProxNivel()
        {
            fase += 1;
            nivelmax += 1;
            dificuldade *= 2;
        }
    }
} 