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

        if (Input.GetKeyDown(Key.M))
        {
            ((MyGame)game).LoadLevel(levelName);
        }

    }

    void Update()
    {
        CheckIfClicked();
    }
}

