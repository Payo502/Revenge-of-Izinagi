using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;


public class Ghost : Enemy
{

    int ghostScore = 300;

    public Ghost() : base("ghost.png", 3, 1, 6, 20,5)
    {
        scale= 0.75f;
    }

    protected override void AddScore()
    {
        if (player.HasDoubleXP())
        {
            Player.score += ghostScore * 2;
        }
        else
        {
            Player.score += ghostScore;
        }
        Console.WriteLine(Player.score);
    }

    void Animate()
    {
        
        float dx = HorizonotalMovement(player);
        if(dx < 0)
        {
            AnimateFly();
            Mirror(true, false);
        }
        if(dx > 0)
        {
            AnimateFly();
            Mirror(false, false);
        }
    }

    void AnimateFly()
    {
        SetCycle(0, 3);
        Animate(0.1f);
    }
    public override void ChasePlayer(Player pPlayer, int enemySpeed)
    {
        player = pPlayer;

        float dx = HorizonotalMovement(player);
        float dy = VerticalMovement(player);

        float distance = Mathf.Sqrt(dx * dx + dy * dy);

        Move(dx * enemySpeed / distance, dy * enemySpeed / distance);
    }

    protected override void Update()
    {
        base.Update();
        Animate();
        ChasePlayer(player,enemySpeed);
    }
}

