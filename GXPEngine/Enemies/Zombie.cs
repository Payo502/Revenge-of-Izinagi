using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;
using GXPEngine;


public class Zombie : Enemy
{


    public Zombie() : base("zombie.png", 8, 1, 1, 100, 1)
    {
        scale = 0.75f;
    }

    void Animate()
    {
        float dx = HorizonotalMovement(player);
        if (dx < 0)
        {
            AnimateWalking();
            Mirror(true, false);
        }
        if (dx > 0)
        {
            AnimateWalking();
            Mirror(false, false);
        }
    }

    void AnimateWalking()
    {
        SetCycle(0, 7);
        Animate(0.1f);
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisions();
        Animate();
    }
}

