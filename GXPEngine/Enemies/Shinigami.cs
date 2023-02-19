using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Shinigami : Enemy
{
    int shinigamiScore = 200;
    public Shinigami(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows, 5, 5,2)
    {

    }
    protected override void AddScore()
    {
        Player.score += shinigamiScore;
        Console.WriteLine(Player.score);
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisions();
    }

}

