using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;


public class Oni : Enemy
{
    int oniScore = 500;
    int lastHitTime = 0;
    bool isAttacking = false;

    public Oni() : base("Oni-Sheet.png", 13, 1, 10, 25, 1)
    {
        scale = 0.75f;
    }

    void Animate()
    {
        float dx = HorizonotalMovement(player);
        if (dx < width / 2 && dx > -width/2)
        {
            if (player.x > this.x)
            {
                Mirror(false, false);
            }
            else
            {
                Mirror(true, false);
            }
            AnimateAttack();
            isAttacking= true;
        }
        if (dx > 0 && dx > width/2)
        {
            AnimateWalking();
            Mirror(false, false);
        }
        if (dx < 0 && dx < -width/2)
        {
            AnimateWalking();
            Mirror(true, false);
        }
    }

    void AnimateWalking()
    {
        SetCycle(0, 7);
        Animate(0.1f);
    }

    void AnimateAttack()
    {
        SetCycle(8,5);
        Animate(0.1f);
    }

    protected override void AddScore()
    {
        //base.AddScore();
        Player.score += oniScore;
        Console.WriteLine(Player.score);
    }


    public override void DamagePlayer(Player pPlayer)
    {
        player = pPlayer;
        GameObject[] collisions = GetCollisions();
        foreach (GameObject col in collisions)
        {
            if (col is Player)
            {
                if (isAttacking && Time.time - lastHitTime >= 2000f)
                {
                    lastHitTime = Time.time;
                    player.TakeDamage(attackPower);
                    Console.WriteLine(attackPower);
                }
            }
        }
    }



protected override void Update()
{
    base.Update();
    Animate();
}
    
}

