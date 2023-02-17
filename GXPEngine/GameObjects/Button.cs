using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

internal class Button : AnimationSprite
{
    string levelName;

    public Button(string spritename, int cols, int rows, TiledObject obj) : base(spritename, cols, rows)
    {
        levelName = obj.GetStringProperty("Name");
    }

    void CheckIfClicked()
    {
        if (HitTestPoint(Input.mouseX, Input.mouseY))
        {
            if (Input.GetMouseButton(0))
            {
                ((MyGame)game).LoadLevel(levelName);
            }
        }
    }

    void Update()
    {
        CheckIfClicked();
    }
}

