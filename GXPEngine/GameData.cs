using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GXPEngine;

public class GameData : GameObject
{
    public int score;

    public GameData() 
    { 
        Reset();
    }

    public void Reset()
    {
        score= 0;
    }
}

