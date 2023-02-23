using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;
class StartButton : Button
{
    private string levelName;
    private string levelName2;
    public StartButton(string filename, int cols, int rows, TiledObject obj = null):base("start_menu.png", 2, 1)
    {
        levelName = obj.GetStringProperty("Name");
        levelName2 = obj.GetStringProperty("Name2");
    }

    void Animate()
    {
        SetCycle(0,2);
        Animate(0.1f);
    }

    protected override void DoStuff()
    {
        ((MyGame)game).LoadLevel(levelName);
    }
    protected override void DoMenuStuff()
    {
        ((MyGame)game).LoadLevel(levelName2);
    }

    void Update()
    {
        CheckIfClicked();
        Animate();
    }
}


