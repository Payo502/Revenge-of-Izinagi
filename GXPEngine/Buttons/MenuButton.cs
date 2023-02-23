using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;
class MenuButton : Button
{
    private string levelName;
    private string levelName2;
    public MenuButton(string filename, int cols, int rows, TiledObject obj = null) : base("Menu.png", 1, 1)
    {
        levelName = obj.GetStringProperty("Name");
        levelName2 = obj.GetStringProperty("Name2");

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

    }
}