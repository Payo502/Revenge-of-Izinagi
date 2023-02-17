using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

public class Pickup : AnimationSprite
{
    float gravity = 0.3f;
    float vy;

    Player player;

    public Pickup(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows)
    {
        collider.isTrigger = true;
        SetOrigin(width / 2, height / 2);
    }

    public void Grab()
    {
        Console.WriteLine("Pickup grabbed!");
        LateDestroy();
    }

    protected void Gravity()
    {
        vy += gravity;

        Collision col = MoveUntilCollision(0, vy);
        if (col != null)
        {
            vy = 0;
        }
    }

    void Update()
    {
        Gravity();
    }

    public void CheckIfPickedByPlayer(Player pPlayer)
    {
        pPlayer = player;
        GameObject[] collisions = GetCollisions();
        foreach (GameObject col in collisions)
        {
            if (col is Player)
            {
                Grab();
            }
        }
    }

}

