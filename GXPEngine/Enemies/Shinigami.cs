using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Shinigami : Enemy
{
    public Shinigami(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows, 5, Utils.Random(1, 5), 3)
    {

    }

    void CheckCollisions()
    {
        GameObject[] collisions = GetCollisions();
        foreach (GameObject col in collisions)
        {
            if (col is Bullet)
            {
                TakeDamage(1);
                col.Destroy();
                Console.WriteLine("Shinigami health remaining {0}", health);
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisions();
    }

}

