using System;
using GXPEngine;
using System.Drawing;
using TiledMapParser;
using GXPEngine.Managers;
using System.Collections.Generic;

public class MyGame : Game
{

    string startLevel = "mapLevel1.tmx";

    string nextLevel = null;
    GameData gameData;


    public readonly PlayerData playerData;

    public MyGame() : base(683, 384, false, false,1366,768,true)
    {
        gameData = new GameData();
        playerData = new PlayerData();

        LoadLevel(startLevel);

        OnAfterStep += CheckLevel;

        game.RenderMain = false;

    }

    void DestroyAll()
    {
        List<GameObject> children = GetChildren();
        foreach (GameObject child in children)
        {
            child.Destroy();
        }
    }

    void ResetGame()
    {
        if (Input.GetKeyDown(Key.R))
        {
            Console.WriteLine("Reloading + starting " + startLevel);
            LoadLevel(startLevel);

        }
    }

    public void LoadLevel(string filename)
    {
        nextLevel = filename;
    }

    void CheckLevel()
    {
        if (nextLevel != null)
        {
            DestroyAll();
            AddChild(new Level(nextLevel,gameData));
            AddChild(new HUD());
            nextLevel = null;
        }
    }


    void Update()
    {
        ResetGame();
    }

    static void Main()
    {
        new MyGame().Start();
    }
}