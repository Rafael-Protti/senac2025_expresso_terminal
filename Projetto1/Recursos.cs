using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace Projetto1
{
    public class Recursos
    {
        public int combustivel;
        public int carga;
        public Recursos(int combustivel, int carga)
        {
            this.combustivel = combustivel;
            this.carga = carga;
            /*this.comb_perda = comb_perda;
            this.carg_perda = carg_perda;
            this.pos = pos;
            this.listax = listax;
            this.listay = listay;*/

        }
        public void Perda(Vector2 pos, List<int> listax, List<int> listay, int comb_perda, int carg_perda)
        {
            for (int i = 0; i < listax.Count; i++)
            {
                if (pos.x + 16 >= listax[i] && pos.x <= listax[i] && pos.y + 3 == listay[i])
                { PerderCarga(carg_perda); PerderCombustivel(comb_perda); }
            }
        }
        public void PerderCombustivel(int comb_perda)
        {
            combustivel -= comb_perda;
        }

        public void PerderCarga(int carg_perda)
        {
            carga -= carg_perda;
        }
    }

}