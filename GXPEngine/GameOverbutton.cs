using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;
class GameOverButton : Button
{
    private string levelName;
    public GameOverButton(string filename, int cols, int rows, TiledObject obj = null) : base("game_over.png", 2, 1)
    {
        levelName = obj.GetStringProperty("Name");
    }

    void Animate()
    {
        SetCycle(0, 2);
        Animate(0.1f);
    }

    protected override void DoStuff()
    {
        ((MyGame)game).LoadLevel(levelName);
    }

    void Update()
    {
        CheckIfClicked();
        Animate();
    }
}