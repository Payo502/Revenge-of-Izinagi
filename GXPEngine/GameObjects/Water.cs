using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Water : AnimationSprite
{
    public const float slowDownFactor = 0.5f;
    public Water(string filename,int cols, int rows,TiledObject obj = null) : base(filename,cols,rows)
    {
        SetOrigin(width / 2, height / 2);
    }
    

}

