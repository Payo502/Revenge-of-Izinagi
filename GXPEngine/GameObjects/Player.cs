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

    public static int score = 0;

    const int maxHealth = 100;
    public int health;

    float speedX = 7f;

    int bulletSpeed = 9;

    int jumpStrength = 11;
    float gravity = 0.3f;
    float vy;

    bool grounded = false;

    HUD hud = null;
    GameData gamedata;

    float dashTime = 500f;
    float dashSpeed = 1f;
    bool isDashing = false;
    float dashTimer = 0f;
    bool facingLeft;

    int dashDelay = 500;
    int lastDash = 0;

    int shootDelay = 150;
    int lastShoot = 0;

    bool isBlocking = false;
    int blockDelay = 2000;
    int lastBlock = 0;

    Sound katanaSound = new Sound("katana_woosh.mp3", false, false);
    Sound dyingSound = new Sound("player_dying.mp3", false, false);

    public Player(String filename, int cols, int rows, TiledObject obj = null) : base("Ninja.png", 14, 1)
    {
        collider.isTrigger = true;
        SetOrigin(width / 2, height / 2);
        SetCycle(13, 2); // Idle Animation

        health = maxHealth;
    }

    float GetHorizonatalMovement()
    {
        float dx = 0;

        if (Input.GetKeyDown(Key.L) && !isDashing && !isBlocking)
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
            AnimateDashing();
        }
        else if (isBlocking)
        {
            AnimateBlocking();
        }
        else if (vy < 0)
        {
            AnimateJumping();
        }
        else if (vy > 0)
        {
            AnimateFalling();
        }
        else if (dx != 0)
        {
            AnimateRunning();
        }
        else
        {
            AnimateIdle();
        }
    }

    void AnimateDashing()
    {
        SetCycle(11, 1); // Dashing Animation
        Animate(0.3f);
    }

    void AnimateBlocking()
    {
        SetCycle(8, 1); // Blocking Animation
        Animate(0.3f);
    }

    void AnimateJumping()
    {
        SetCycle(8, 3); // Jumping Animation
        Animate(0.08f);
    }

    void AnimateFalling()
    {
        SetCycle(10, 1); // Falling Animation
        Animate(0.3f);
    }

    void AnimateRunning()
    {
        SetCycle(0, 7); // Running Animation
        Animate(0.25f);
    }

    void AnimateIdle()
    {
        SetCycle(12, 2); // Idle Animation
        Animate(0.1f);
    }

    public void TakeDamage(int damage)
    {
        if (!isBlocking && !isDashing)
        {
            health -= damage;
            float percentage = 1f * health / maxHealth;
            hud.AddPlayerHealthBar(percentage, health);
            if (health <= 0)
            {
                dyingSound.Play();
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
                    katanaSound.Play();
                    Bullet bullet = new Bullet(_mirrorX ? -bulletSpeed : bulletSpeed, 0, this);
                    bullet.SetXY(x + (_mirrorX ? -1 : 1) * width / 2, y);
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
        hud.SetXY(x-game.width/2 - width/2,y - game.height/2 - 20);
        Block();
        //SetupHUD();
    }
}

