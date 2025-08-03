using System;

namespace Projetto1;

public class Obstaculos
{
    public Pixel forma;
    public int quantia;

    public Vector2 pos = new Vector2(1, 1);

    public Random random = new Random();

    public int distancia;

    public Pixel[,] matriz;

    public Obstaculos(Pixel forma, int quantia)
    {
        this.forma = forma;
        this.quantia = quantia;
        //this.matriz = matriz;
    }

    public void Randomizer() //Criar listas que armazenam os valores dos lugares antigos dos obstáculos. Usa essas lista para não repetir posições.
    {
        pos.x = random.Next(20, 155);
        if (random.Next(2) == 0)
        {
            pos.y = 5;
        }
        else
        {
            pos.y = 10;
        }
        distancia = pos.x + random.Next(1, 11);
    }
}
