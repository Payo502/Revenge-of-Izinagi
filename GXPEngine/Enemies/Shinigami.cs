using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Shinigami : Enemy
{
    int shinigamiScore = 200;
    public Shinigami() : base("shinigami.png", 8, 1, 6, 20, 6)
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
    protected override void AddScore()
    {
        Player.score += shinigamiScore;
        Console.WriteLine(Player.score);
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisions();
        Animate();
    }

}

