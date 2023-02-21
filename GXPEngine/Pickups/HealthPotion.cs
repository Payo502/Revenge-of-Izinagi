using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class HealthPotion : Pickup
{
    Player player;
    public HealthPotion(Player pPlayer) : base("healthPotion.png", 6, 1)
    {
        collider.isTrigger = true;
        SetOrigin(width / 2, height / 2);
        player = pPlayer;
    }


    protected override void Grab()
    {
        base.Grab();
        Console.WriteLine("Health Potion grabbed");
        if (player.health < 100)
        {
            player.health = 100;
        }
        Console.WriteLine("Player health is: {0}", player.health);

    }
}

