using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using System.Xml;
using System.Drawing;


public class HUD : GameObject
{
    EasyDraw healthBar;
    EasyDraw scoreBoard;

    GameData gameData;

    public HUD()
    {
        healthBar = new EasyDraw(100, 100, false);
        AddChild(healthBar);

        scoreBoard = new EasyDraw(100, 20, false);
        scoreBoard.SetXY(game.width - scoreBoard.width, 0);
        AddChild(scoreBoard);

    }

    public void AddPlayerHealthBar(float healthPercentage, int playerHealth)
    {
        healthBar.graphics.Clear(Color.Empty);
        healthBar.ShapeAlign(CenterMode.Min, CenterMode.Min);
        healthBar.NoStroke();
        healthBar.Fill(0, 255, 0);
        healthBar.Rect(0, 0, 80.0f * healthPercentage, 10);
        healthBar.StrokeWeight(1);
        healthBar.NoFill();
        healthBar.Rect(0, 0, 80.0f, 0);
        healthBar.TextSize(7);
        healthBar.Fill(255);
        healthBar.TextAlign(CenterMode.Min, CenterMode.Min);
        healthBar.Text(String.Format("              {0}", playerHealth));
        healthBar.Stroke(1);
        healthBar.NoFill();
        healthBar.Rect(0, 0, 80.0f, 10);

    }

    public void SetScore(float enemyScore)
    {
        scoreBoard.Text(String.Format("Score: {0}", enemyScore), true);
    }

    public void SetGameData(GameData pGameData)
    {
        gameData = pGameData;
    }

}

