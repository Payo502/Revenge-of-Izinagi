using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;


public class DoubleXP : Pickup
{
    Player player;
    public DoubleXP(Player pPlayer) : base("2X.png", 6, 1)
    {
        collider.isTrigger = true;
        SetOrigin(width / 2, height / 2);
        player = pPlayer;
        scale = 0.75f;
        player.ActivateDoubleXP(10000f);
    }

    protected override void Grab()
    {
        base.Grab();
        Console.WriteLine("DoubleXP Potion grabbed");

        
    }

}

