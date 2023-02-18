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


    public Ghost(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows, 5, 1, 3)
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
                Console.WriteLine("Ghost health remaining {0}", health);
            }
        }
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
        CheckCollisions();
    }
}

