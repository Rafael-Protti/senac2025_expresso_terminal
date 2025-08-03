using System;
using System.Security.Cryptography.X509Certificates;

namespace Projetto1;

public class Obstaculos
{
    public Pixel forma;
    public int quantia;

    public Vector2 pos = new Vector2(1, 1);
    public int[] registro = new int[] {}; 

    public Random random = new Random();

    public Pixel[,] matriz;

    public Obstaculos(Pixel forma, int quantia)
    {
        this.forma = forma;
        this.quantia = quantia;
        //this.matriz = matriz;
    }

    public void Randomizer() //Criar listas que armazenam os valores dos lugares antigos dos obstáculos. Usa essas lista para não repetir posições.
    {
        int numero;
        do
        {
            numero = random.Next(20, 165);
            if (registro.All(x => x != numero && x + 10 < numero && x - 10 > numero))
            {
                registro.Append(numero);
                pos.x = numero;
            }
        } while (!registro.All(x => x != pos.x));
        if (random.Next(2) == 0)
        {
            pos.y = 5;
        }
        else
        {
            pos.y = 10;
        }
    }
}
