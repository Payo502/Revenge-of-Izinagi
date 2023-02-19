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


    public Level(string filename, GameData pGameData)
    {
        gameData = pGameData;
        Console.WriteLine("---Creating new Level Object");
        currentLevelName = filename;
        loader = new TiledLoader(filename);

        hud.SetGameData(gameData);
        this.AddChild(hud);

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

        AddChild(hud);

        player = FindObjectOfType<Player>();
        if (player != null)
        {
            Console.WriteLine("Player found!!");
            player.PlayerDead += PlayerDeath;

        }
        else
        {
            Console.WriteLine("Player is not found!");
        }

        enemySpawn = new EnemySpawnManager();
        AddChild(enemySpawn);
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
            ((MyGame)game).LoadLevel("mapLevel1.tmx");
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
        Scrolling();
        SetPlayer();
        //SpawnEnemyAtPosition();
    }


}

