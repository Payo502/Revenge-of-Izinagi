using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;


public class Lava : AnimationSprite
{
    Player player;

    public Lava(string filename, int cols, int rows,TiledObject obj = null) : base("checkers.png", 1,1)
    {
        SetOrigin(width/2,height/2);
    }

    void CheckCollisions()
    {
        GameObject[] collisions = GetCollisions();
        foreach (GameObject cols in collisions)
        {
            if (cols is Player)
            {
                player.TakeDamage(1);
                Console.WriteLine("Touching Lava");
            }
        }
    }

    void Update()
    {
        CheckCollisions();
    }
}

