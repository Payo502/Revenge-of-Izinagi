using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;


public class Oni : Enemy
{
    public Oni(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows, 10, 5, 1,100)
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
                Console.WriteLine("Oni health remaining {0}", health);
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisions();
    }
}

