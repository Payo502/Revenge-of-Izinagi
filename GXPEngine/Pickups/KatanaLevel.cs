using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;


public class KatanaLevel : Pickup
{
    public KatanaLevel() : base("katana_upgrade.png", 13,1)
    {
        SetOrigin(width/2,height/2);
        scale = 0.40f;
    }

    protected override void Grab()
    {
        base.Grab();
        Console.WriteLine("Katana upgrade has been picked up");
        if(Player.katanaLevel <= 2)
        {
            Player.katanaLevel += 1;
        }
        Console.WriteLine("Katana level is: {0}", Player.katanaLevel);

    }
}

