using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Bullet : Sprite
{
    float vx, vy;

    GameObject owner;

    float creationTime = Time.time;
    float bulletDestroyDelay = 5000;



    public Bullet(float pVx, float pVy, GameObject pOwner) : base("bullet.png")
    {
        collider.isTrigger = true;
        SetOrigin(width / 2, height / 2);
        this.vx = pVx;
        this.vy = pVy;
        owner = pOwner;

    }

    void Move()
    {
        x += vx;
        y += vy;

        CheckCollisions();

        DestroyBullet();
    }

    void CheckCollisions()
    {
        GameObject[] collisions = GetCollisions();
        foreach (GameObject col in collisions)
        {

        }
    }

    void DestroyBullet()
    {
        if(Time.time > creationTime + bulletDestroyDelay)
        {
            LateDestroy();
        }
    }


    void Update()
    {
        Move();
        
    }

}

