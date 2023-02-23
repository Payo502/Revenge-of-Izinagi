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
    Vector2 spawnPoint1 = new Vector2(256, 736);
    Vector2 spawnPoint2 = new Vector2(2816, 768);

    float spawnDelay = 4000;
    float spawnDelayDecrement = 50;
    float lastSpawnTime = 0f;


    Enemy[] enemyType = new Enemy[]
    {
        new Zombie(),
        new Shinigami(),
        new Oni(),
        new Ghost()
    };


    int[] enemyProbabilities = new int[] { 60, 15, 5, 20 }; //{ 60, 15, 5, 20 }
    int[] enemyProbabilityChange = new int[] { -2, 1, 2, 0 };


    List<Enemy> activeEnemies = new List<Enemy>();

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

        Enemy enemy = Enemy.Clone(enemyOfType);
        //Zombie enemy = new Zombie("triangle.png", 1, 1);
        enemy.SetXY(position.x, position.y);
        parent.AddChild(enemy);
        activeEnemies.Add(enemy);
        Console.WriteLine(activeEnemies);

    }

    private void CheckSpawnEnemy()
    {

        if (Time.time > lastSpawnTime + spawnDelay)
        {
            lastSpawnTime = Time.time;
            int rand = Utils.Random(0, 100);
            Enemy enemyToSpawn = GetRandomEnemy();
            Vector2 spawnPoint = rand < 50 ? new Vector2(200, 300) : new Vector2(2000, 300);
            SpawnEnemyAtPosition(spawnPoint, enemyToSpawn);

            //Decrement spawn delay
            if(spawnDelay < 2000)
            {
                spawnDelayDecrement= 0;
            }
            spawnDelay -= spawnDelayDecrement;
            Console.WriteLine("console spawn delay " + spawnDelay);

            // Adjust enemy probabilities over time
            for (int i = 0; i < enemyProbabilities.Length; i++)
            {
                enemyProbabilities[i] += enemyProbabilityChange[i];
                //Console.WriteLine("Enemy probabilities "+ enemyProbabilities);
            }
        }
    }


    private void CheckActiveEnemies()
    {
        for (int i = activeEnemies.Count - 1; i >= 0; i--)
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
