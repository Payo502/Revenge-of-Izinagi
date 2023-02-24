using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;


public class Level : GameObject
{
    string currentLevelName;
    TiledLoader loader;
    Player player;
    GameData gameData;

    bool respawn = false;

    HUD hud = new HUD();
    EnemySpawnManager enemySpawn;

    Sound backgroundMusic = new Sound("Epic_Japanese_Music_Shadow_Ninja.mp3", false, true);

    public Level(string filename, GameData pGameData)
    {
        gameData = pGameData;
        Console.WriteLine("---Creating new Level Object");
        currentLevelName = filename;
        loader = new TiledLoader(filename);


        //hud.SetGameData(gameData);
        

        CreateLevel();
    }

    void CreateLevel(bool includeImageLayers = true)
    {
        Console.WriteLine("Spawning level elements: ");

        loader.autoInstance = true;
        loader.addColliders = false;
        //loader.rootObject = game;
        loader.rootObject = this;
        if (includeImageLayers) loader.LoadImageLayers();
        loader.rootObject = this;
        loader.LoadTileLayers(0);
        loader.addColliders = true;
        loader.LoadTileLayers(1);
        loader.LoadObjectGroups();


       


        player = FindObjectOfType<Player>();
        if (player != null)
        {

            Console.WriteLine("Player found!!");
            AddChild(hud);

            enemySpawn = new EnemySpawnManager();
            AddChild(enemySpawn);

            Camera cam1 = new Camera(0, 0, 683, 500);
            cam1.scale = 1.5f;
            player.AddChild(cam1);
            player.PlayerDead += PlayerDeath;
            backgroundMusic.Play();

        }
        else
        {
            Console.WriteLine("Player is not found!");
            Camera cam2 = new Camera(0, 0, 683, 384);
            if(currentLevelName == "gameOver.tmx")
            {
                hud.FinalScore(Player.score);
            }
            cam2.SetXY(350, 192);
            AddChild(cam2);
        }

    }

    public void SetPlayer()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.ChasePlayer(player, enemy.enemySpeed);
            enemy.DamagePlayer(player);


        }

        Pickup[] pickups = FindObjectsOfType<Pickup>();
        foreach (Pickup pickup in pickups)
        {
            pickup.CheckIfPickedByPlayer(player);
        }

        Player[] players = FindObjectsOfType<Player>();
        foreach (Player player in players)
        {
            player.SetHUD(hud);

        }
    }



    public void PlayerDeath()
    {
        PlayerData data = ((MyGame)game).playerData;

        if (data.lives <= 0 || respawn)
        {
            return;
        }
        data.lives--;
        respawn = true;

        player.PlayerDead -= PlayerDeath;


        if (data.lives <= 0)
        {
            data.Reset();
            Player.score = 0;
            Player.katanaLevel = 0;
            ((MyGame)game).LoadLevel("gameOver.tmx");
        }

        else
        {
            ((MyGame)game).LoadLevel(currentLevelName);
        }

    }

    void Scrolling()
    {
        int boundary = game.width / 2;
        int bottomboundary = 128;
        if (player != null && player.x + x < boundary)
        {
            x = boundary - player.x;
        }
        if (player != null && player.x + x > game.width - boundary)
        {
            x = game.width - boundary - player.x;
        }
        if (player != null && player.y + y < bottomboundary)
        {
            y = bottomboundary - player.y;
        }
        if (player != null && player.y + y > game.height - bottomboundary)
        {
            y = game.height - bottomboundary - player.y;
        }
    }

    void Update()
    {
        //Scrolling();
        SetPlayer();
        //SpawnEnemyAtPosition();
    }


}

