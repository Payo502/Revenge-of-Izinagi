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
    HUD scoreHUD;

    protected int health;
    protected int attackPower;
    public int enemySpeed;

    int delay = 1000;
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
            clone = new Zombie();
        }
        else if (original is Shinigami)
        {
            clone = new Shinigami();
        }
        else if (original is Oni)
        {
            clone = new Oni();
        }
        else if (original is Ghost)
        {
            clone = new Ghost();
        }
        clone.SetXY(original.x, original.y);
        return clone;
    }

    protected void CheckCollisions()
    {
        GameObject[] collisions = GetCollisions();
        foreach (GameObject col in collisions)
        {
            switch (Player.katanaLevel)
            {
                case 0:
                    if (col is Bullet)
                    {
                        TakeDamage(1);
                        col.Destroy();
                    }
                    break;
                case 1 :
                    if (col is Bullet)
                    {
                        TakeDamage(1);
                        col.Destroy();
                    }
                    break;
                case 2:
                    if (col is Bullet)
                    {
                        TakeDamage(2);
                        col.Destroy();
                    }
                    break;
                case 3:
                    if (col is Bullet)
                    {
                        TakeDamage(3);
                        col.Destroy();
                    }
                    break;
            }
        }
    }


    protected void TakeDamage(int damage)
    {
        health -= damage;

        //scoreHUD.SetScore(Player.score);

        if (health <= 0)
        {
            Die();
        }

        ShowHealthBar();
    }

    public virtual void DamagePlayer(Player pPlayer)
    {
        player = pPlayer;
        GameObject[] collisions = GetCollisions();
        foreach (GameObject col in collisions)
        {
            if (col is Player)
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
        DoubleXP doubleXP;
        KatanaLevel katanaLevel;
        int randPotion = Utils.Random(0, 6);
        switch (randPotion)
        {
            case 0:
                katanaLevel = new KatanaLevel();
                parent.AddChild(katanaLevel);
                katanaLevel.SetXY(x, y);
                break;
            case 1:
                doubleXP = new DoubleXP(player);
                parent.AddChild(doubleXP);
                doubleXP.SetXY(x, y);
                break;
            case 2:
                healthPotion = new HealthPotion(player);
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
        Console.WriteLine("Highscore: {0}", Player.score);
    }

    protected float HorizonotalMovement(Player pPlayer)
    {
        player = pPlayer;
        float dx = 0;
        if (player != null)
        {
            dx = player.x - x;
        }
        return dx;
    }

    public float VerticalMovement(Player pPlayer)
    {
        player = pPlayer;
        float dy = 0;
        if (player != null)
        {
            dy = player.y - y;
        }
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
        //scoreHUD.SetXY(0,0);
        CheckCollisions();
    }



}
