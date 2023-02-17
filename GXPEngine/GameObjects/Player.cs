using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

public class Player : AnimationSprite
{
    public event Action PlayerDead;

    const int maxHealth = 10;
    int health;

    float speedX = 5f;

    int bulletSpeed = 8;

    int jumpStrength = 10;
    float gravity = 0.3f;
    float vy;

    bool grounded = false;

    HUD hud = null;
    GameData gamedata;

    float dashTime = 500f;
    float dashSpeed = 0.5f;
    bool isDashing = false;
    float dashTimer = 0f;
    bool facingLeft;

    int dashDelay = 1000;
    int lastDash = 0;

    int shootDelay = 500;
    int lastShoot = 0;

    bool isBlocking = false;
    int blockDelay = 2000;
    int lastBlock = 0;


    public Player(TiledObject obj = null) : base("Ninja.png", 10, 1)
    {
        collider.isTrigger = true;
        SetOrigin(width / 2, height / 2);
        SetCycle(0, 1); // Idle Animation

        health = maxHealth;
    }

    float GetHorizonatalMovement()
    {
        float dx = 0;

        if (Input.GetKeyDown(Key.L) && !isDashing)
        {
            if (Time.time > lastDash + dashDelay)
            {
                lastDash = Time.time;
                isDashing = true;
                dashTimer = dashTime;

            }
        }

        if (!isDashing)
        {
            if (Input.GetKey(Key.A))
            {
                dx = -speedX;
                Mirror(true, false);
                facingLeft = true;
            }
            if (Input.GetKey(Key.D))
            {
                dx += speedX;
                Mirror(false, false);
                facingLeft = false;
            }
        }
        else
        {
            dx = dashSpeed * Time.deltaTime * (facingLeft ? -1 : 1);
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                isDashing = false;
            }
        }
        return dx;
    }


    void Move()
    {

        float dx = GetHorizonatalMovement();

        vy += gravity;

        if (!isBlocking)
        {
            if (Input.GetKey(Key.K) && grounded) //Original SPACE
            {
                SetCycle(0, 6);
                vy = -jumpStrength;

            }
        }

        grounded = false;
        MoveUntilCollision(dx, 0);
        Collision col = MoveUntilCollision(0, vy);
        if (col != null)
        {
            vy = 0;
            if (col.normal.y < 0)
            {
                grounded = true;
            }
        }

        Animate();
    }

    public void SetHUD(HUD pHud)
    {
        hud = pHud;
        float percentage = 1f * health / maxHealth;
        hud.AddPlayerHealthBar(percentage, health);
    }

    void Animate()
    {
        float dx = GetHorizonatalMovement();

        if (isDashing)
        {
            SetCycle(9, 1); // Dashing Animation
        }
        else if (isBlocking)
        {
            SetCycle(9, 1); // Blocking Animation
        }
        else if (vy < 0)
        {
            SetCycle(7, 2, 3); // Jumping Animation

        }
        else if (vy > 0)
        {
            SetCycle(9, 1); // Falling Animation
        }
        else if (dx != 0)
        {
            SetCycle(0, 6); // Running Animation
        }
        else
        {
            SetCycle(0, 1); // Idle Animation
        }
        Animate(0.3f);
    }

    public void TakeDamage(int damage)
    {
        if (!isBlocking)
        {
            health -= damage;
            float percentage = 1f * health / maxHealth;
            hud.AddPlayerHealthBar(percentage, health);
            if (health <= 0)
            {
                PlayerDead?.Invoke();
            }
        }

    }

    public void Block()
    {
        if (Input.GetKey(Key.S))
        {
            isBlocking = true;
        }
        else
        {
            isBlocking = false;
        }
    }



    public void Shoot()
    {
        if (!isBlocking)
        {
            if (Input.GetKeyDown(Key.J))
            {
                if (Time.time > lastShoot + shootDelay)
                {
                    Bullet bullet = new Bullet(_mirrorX ? -bulletSpeed : bulletSpeed, 0, this);
                    bullet.SetXY(x + (_mirrorX ? -1 : 1) * width / 2, y - height / 5);
                    parent.LateAddChild(bullet);
                    lastShoot = Time.time;
                }

            }
        }
    }

    void Update()
    {
        Move();
        Shoot();
        hud.SetXY(x - width / 2, y - height);
        Block();
        //SetupHUD();
    }
}

