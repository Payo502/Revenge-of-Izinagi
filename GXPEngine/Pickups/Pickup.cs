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

    Sound pickupSound = new Sound("item_pickup.mp3", false, false);

    public Pickup(string filename, int cols, int rows, int healthPotion, TiledObject obj = null) : base(filename, cols, rows)
    {
        collider.isTrigger = true;
        SetOrigin(width / 2, height / 2);
    }

    protected virtual void Grab()
    {
        pickupSound.Play();
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


    protected void Animate()
    {
        Animate(0.1f);
    }

    public void CheckIfPickedByPlayer(Player pPlayer)
    {
        player = pPlayer;
        GameObject[] collisions = GetCollisions();
        foreach (GameObject col in collisions)
        {
            if (col is Player)
            {
                Grab();
            }
        }
    }

    protected void Update()
    {
        Gravity();
        Animate();
    }

}

