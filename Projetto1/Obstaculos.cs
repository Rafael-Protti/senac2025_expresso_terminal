using System;

namespace Projetto1;

public class Obstaculos
{
    public Pixel forma;

    public Vector2 posicao = new Vector2(1, 1);

    public Random random = new Random();

    public int distancia;

    public Pixel[,] matriz;

    public Obstaculos(Pixel forma, Pixel[,] matriz)
    {
        this.forma = forma;
        this.matriz = matriz;
    }

    public void Randomizer()
    {
        posicao.x = random.Next(20, 181);
        if (random.Next(2) == 0)
        {
            posicao.y = 5;
        }
        else
        {
            posicao.y = 10;
        }
        distancia = posicao.x + random.Next(1, 11);
    }

    public void DesenharObstaculos()
    {
        for (int x = posicao.x; x < distancia; x++)
        {
            matriz[x, posicao.y] = forma;
        }
    }
}
