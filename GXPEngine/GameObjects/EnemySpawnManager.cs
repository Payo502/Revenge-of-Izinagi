using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

public class EnemySpawnManager : AnimationSprite
{

    Vector2 spawnPoint1 = new Vector2(100, 100);
    Vector2 spawnPoint2 = new Vector2(500, 500);

    int spawnDelay = 1000;
    int lastSpawnTime = 0;
    int spawnProbability = 50; // in percentage

    public EnemySpawnManager(String filename, int cols, int rows, TiledObject obj = null) : base(filename,cols,rows)
    {
        SetXY(0, 0);
    }

    void Update()
    {
        if (Time.time > lastSpawnTime + spawnDelay)
        {
            lastSpawnTime = Time.time;
            int rand = Utils.Random(0, 100);
            if (rand < spawnProbability)
            {
                SpawnEnemyAtPosition(spawnPoint1);
            }
            else
            {
                SpawnEnemyAtPosition(spawnPoint2);
            }
        }
    }

    public void SpawnEnemyAtPosition(Vector2 position)
    {
        Enemy enemy = new Enemy("enemy.png", 1, 1, 10, 5, 2, 100);
        enemy.SetXY(position.x, position.y);
        parent.AddChild(enemy);
    }
}

