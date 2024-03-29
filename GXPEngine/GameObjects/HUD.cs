﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using System.Xml;
using System.Drawing;


public class HUD : GameObject
{
    //EasyDraw healthBar;
    Sprite healthBarFull;
    EasyDraw scoreBoard;
    EasyDraw lastScore;
    float initialHealthBarWidth;

    EasyDraw levelKatana;

    GameData gameData;


    public HUD()
    {
        //healthBar = new EasyDraw(2000, 100, false);
        //AddChild(healthBar);

        scoreBoard = new EasyDraw(150, 20, false);
        scoreBoard.SetXY(game.width - scoreBoard.width/2, 0);
        AddChild(scoreBoard);

        levelKatana = new EasyDraw(150,20,false);
        levelKatana.SetXY(game.width - levelKatana.width/2, 20);
        AddChild(levelKatana);

        lastScore = new EasyDraw(200, 200, false);
        lastScore.SetXY(game.width/2,game.height/2);
        AddChild(lastScore);


        Sprite UIHealthBar = new Sprite("healthBarEmpty.png", false, false); //the empty healthBar
        //UIHealthBar.SetXY(0, 0);
        healthBarFull = new Sprite("health_bar.png", false, false); //the full healthBar
        //healthBarFull.SetXY(0, 0);

        healthBarFull.height = healthBarFull.height * 2;
        UIHealthBar.height = UIHealthBar.height * 2 ;
        AddChild(healthBarFull);
        AddChild(UIHealthBar);



        initialHealthBarWidth = healthBarFull.width;

    }

    public void AddPlayerHealthBar(float healthPercentage) /*, int playerHealth)*/
    {
        healthBarFull.width = initialHealthBarWidth * healthPercentage;
        
        /*healthBar.graphics.Clear(Color.Empty);
        healthBar.ShapeAlign(CenterMode.Min, CenterMode.Min);
        healthBar.NoStroke();
        healthBar.Fill(0, 255, 0);
        healthBar.Rect(0, 0, 200.0f * healthPercentage, 15);
        healthBar.StrokeWeight(1);
        healthBar.NoFill();
        healthBar.Rect(0, 0, 200.0f, 0);
        healthBar.TextSize(7);
        healthBar.Fill(0);
        healthBar.Stroke(5);
        healthBar.TextAlign(CenterMode.Min, CenterMode.Min);
        healthBar.TextSize(10);
        healthBar.Text(String.Format("                          {0}", playerHealth));
        healthBar.Stroke(1);
        healthBar.NoFill();
        healthBar.Rect(0, 0, 200.0f, 15);*/


    }

    public void SetScore(float playerScore)
    {
        playerScore = Player.score;
        scoreBoard.Text(String.Format("Score: {0}", playerScore), true);
    }

    public void SetKatanaLevel(int katanaLevel)
    {
        katanaLevel = Player.katanaLevel;
        levelKatana.Text(String.Format("Katana Level: {0}", katanaLevel), true);
    }
    public void FinalScore(float finalScore)
    {
        finalScore = Player.score;
        lastScore.Text(String.Format("Highscore: {0}", finalScore), true);

    }

    public void SetGameData(GameData pGameData)
    {
        gameData = pGameData;
    }

}

