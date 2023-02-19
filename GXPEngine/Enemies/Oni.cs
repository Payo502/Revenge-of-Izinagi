using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;


public class Oni : Enemy
{
    int oniScore = 500;

    public Oni(string filename, int cols, int rows, TiledObject obj = null) : base("Oni-Sheet.png", 13, 1, 10, 0, 1)
    {

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
    protected override void AddScore()
    {
        //base.AddScore();
        Player.score += oniScore;
        Console.WriteLine(Player.score);
    }
    protected override void Update()
    {
        base.Update();
        Animate();
    }
}

