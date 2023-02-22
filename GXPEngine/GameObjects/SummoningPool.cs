using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class SummoningPool : AnimationSprite
{
    public SummoningPool(string filename, int cols, int rows, TiledObject obj = null) : base("summoning_pool.png", 9, 1,-1,false,false)
    {
        SetOrigin(width / 2, height / 2);
        SetCycle(0,8);
    }
    void Animate()
    {
        SetCycle(0,8);
        Animate(0.5f);
    }

    void Update()
    {
        Animate();
    }
    
}

