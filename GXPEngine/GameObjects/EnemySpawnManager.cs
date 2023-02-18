using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

public class EnemySpawnManager : GameObject
{

    Vector2 spawnPoint1 = new Vector2(200, 300);
    Vector2 spawnPoint2 = new Vector2(2000, 300);

    int spawnDelay = 6000;
    int lastSpawnTime = 0;

    int maxEnemies = 10;

    Enemy[] enemyType = new Enemy[]
    {
        new Zombie("square.png",1,1),
        new Shinigami("checkers.png",1,1),
        new Oni("Oni-75%.png",5,1),
        new Ghost("circle.png",1,1)
    };
    List<Enemy> activeEnemies = new List<Enemy>();
    int[] enemyProbabilities = new int[] { 100, 0, 0, 0 };

    public EnemySpawnManager() : base(false)
    {
        SetXY(0, 0);
        lastSpawnTime = Time.time;
    }

    private Enemy GetRandomEnemy()
    {

        int index = Utils.Random(0, 100);
        int sum = 0;
        for (int i = 0; i < enemyProbabilities.Length; i++)
        {
            sum += enemyProbabilities[i];
            if (index < sum)
            {
                return enemyType[i];
            }
        }
        return enemyType[enemyType.Length - 1];
    }

    private void SpawnEnemyAtPosition(Vector2 position, Enemy enemyOfType)
    {
        if (activeEnemies.Count >= maxEnemies)
        {
            return;
        }
        Enemy enemy = Enemy.Clone(enemyOfType);
        //Zombie enemy = new Zombie("triangle.png", 1, 1);
        enemy.SetXY(position.x, position.y);
        parent.AddChild(enemy);
        activeEnemies.Add(enemy);
    }

    private void CheckSpawnEnemy()
    {
        if (Time.time > lastSpawnTime + spawnDelay)
        {
            lastSpawnTime = Time.time;
            int rand = Utils.Random(0, 100);
            Enemy enemyToSpawn = GetRandomEnemy();
            if (rand < 50)
            {
                SpawnEnemyAtPosition(spawnPoint1, enemyToSpawn);
            }
            else
            {
                SpawnEnemyAtPosition(spawnPoint2, enemyToSpawn);
            }
        }
    }



    private void CheckActiveEnemies()
    {
        for(int i = activeEnemies.Count -1; i >= 0; i--)
        {
            if (activeEnemies[i] == null)
            {
                activeEnemies.RemoveAt(i);
            }
        }
    }

    void Update()
    {
        CheckSpawnEnemy();
        CheckActiveEnemies();
    }

}
