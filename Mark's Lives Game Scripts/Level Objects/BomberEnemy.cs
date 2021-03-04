using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemy : FlyingEnemy
{
    [SerializeField]
    private float rate = 2.0F;
    [SerializeField]
    private Sprite bulletImg;
    private FallingBullet bullet;

    protected override void Awake()
    {
        base.Awake();
        bullet = Resources.Load<FallingBullet>("FallingBullet");
    }

    protected override void Start()
    {
        pos = transform.position;
        localScale = transform.localScale;
        dirX = -1.0f;
        centerPos = transform.position.x;

        InvokeRepeating("Shoot", rate, rate);
    }

    private void Shoot()
    {
        if (!isDefeated)
        {
            Vector3 position = transform.position; position.y -= 0.2F;
            FallingBullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as FallingBullet;

            newBullet.Parent = gameObject;
            newBullet.spriteImg = bulletImg;
            newBullet.bulletSpeed = 5.0f;
            newBullet.gameObject.tag = "FallingObstacle";
            newBullet.Direction = -newBullet.transform.up;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isDefeated)
        {
            Bullet bullet = collider.GetComponent<Bullet>();

            if (bullet && bullet.Parent != gameObject)
            {
                ReceiveDamage();
            }
        }
    }
}
