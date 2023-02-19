using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;


public class Ghost : Enemy
{
    Player player;

    int ghostScore = 300;

    public Ghost(string filename, int cols, int rows, TiledObject obj = null) : base("ghost.png",3,1, 5, 0)
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
        SetCycle(0, 3);
        Animate(0.1f);
    }

    protected override void AddScore()
    {
        Player.score += ghostScore;
        Console.WriteLine(Player.score);
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
        //Animate();
        ChasePlayer(player,enemySpeed);
    }
}

