using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
public class Enemy : AnimationSprite
{
    float gravity = 0.3f;
    float vy;

    protected Player player;

    protected int health;
    protected int attackPower;
    public int enemySpeed;

    int delay = 500;
    int lastHitTime = 0;


    EasyDraw hpBar = new EasyDraw(100, 100, false);


    public Enemy(string filename, int cols, int rows, int health = 0, int attackPower = 0, int enemySpeed = 0, TiledObject obj = null) : base(filename, cols, rows)
    {
        collider.isTrigger = true;
        SetOrigin(width / 2, height / 2);
        this.health = health;
        this.attackPower = attackPower;
        this.enemySpeed = enemySpeed;


        AddChild(hpBar);
    }

    protected void Gravity()
    {
        vy += gravity;

        Collision col = MoveUntilCollision(0, vy);
        if (col != null)
        {
            vy = 0;
        }
    }


    public static Enemy Clone(Enemy original)
    {
        Enemy clone = null;
        if (original is Zombie)
        {
            clone = new Zombie("zombie.png", 5, 1);
        }
        else if (original is Shinigami)
        {
            clone = new Shinigami("checkers.png", 1, 1);
        }
        else if (original is Oni)
        {
            clone = new Oni("Oni-Sheet.png", 13, 1);
        }
        else if (original is Ghost)
        {
            clone = new Ghost("circle.png", 1, 1);
        }
        clone.SetXY(original.x, original.y);
        return clone;
    }

    protected void CheckCollisions()
    {
        GameObject[] collisions = GetCollisions();
        foreach (GameObject col in collisions)
        {
            if (col is Bullet)
            {
                TakeDamage(1);
                col.Destroy();
            }
        }
    }


    protected void TakeDamage(int damage)
    {
        health -= damage;


        if (health <= 0)
        {
            Die();
        }

        ShowHealthBar();
    }

    public void DamagePlayer(Player pPlayer)
    {
        player = pPlayer;
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i] is Player)
            {
                if (Time.time > lastHitTime + delay)
                {
                    player.TakeDamage(attackPower);
                    lastHitTime = Time.time;
                    Console.WriteLine(attackPower);
                }
            }
        }

    }

    protected void Die()
    {
        HealthPotion healthPotion;
        int randPotion = Utils.Random(0, 2);
        switch (randPotion)
        {
            case 0:
                healthPotion = new HealthPotion("healthPotion.png", 6, 1,player);
                parent.AddChild(healthPotion);
                healthPotion.SetXY(x, y);
                break;

        }
        AddScore();

        LateDestroy();
    }

    protected virtual void AddScore()
    {
        Player.score += 100;
        Console.WriteLine(Player.score);
    }

    protected float HorizonotalMovement(Player pPlayer)
    {
        player = pPlayer;
        float dx = player.x - x;
        Console.WriteLine(dx);
        return dx;
    }

    public float VerticalMovement(Player pPlayer)
    {
        player = pPlayer;
        float dy = player.y - y;
        return dy;
    }

    public virtual void ChasePlayer(Player pPlayer, int enemySpeed)
    {
        player = pPlayer;


        float dx = HorizonotalMovement(player);
        float dy = VerticalMovement(player);

        float distance = Mathf.Sqrt(dx * dx + dy * dy);


        Move(dx * enemySpeed / distance, 0);


        Gravity();

    }

    void ShowHealthBar()
    {
        hpBar.graphics.Clear(Color.Empty);
        hpBar.ShapeAlign(CenterMode.Min, CenterMode.Min);
        hpBar.NoStroke();
        hpBar.Fill(255, 0, 0);
        hpBar.Rect(0, 0, 20.0f * (float)health / (float)5, 3);
    }

    public int GetAttackPower()
    {
        return attackPower;
    }

    protected virtual void Update()
    {
        //CheckCollisions();
        //DamagePlayer(player);
        hpBar.SetXY(0 - 10, 0);
        CheckCollisions();
    }



}
