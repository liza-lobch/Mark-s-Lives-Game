using System.Collections;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    [SerializeField]
    protected float rate = 2.0F;
    [SerializeField]
    protected Sprite bulletImg;
    protected Bullet bullet;

    protected Character character;
    protected float localScaleX;

    protected bool facingRight = true;

    protected override void Awake()
    {
        base.Awake();
        bullet = Resources.Load<Bullet>("Bullet");
    }

    protected override void Start()
    {
        InvokeRepeating("Shoot", rate, rate);

        character = FindObjectOfType<Character>();

        localScaleX = transform.localScale.x;
    }

    protected virtual void LateUpdate()
    {
        if (!isDefeated)
        {
            CheckWhereToFace();
        }
    }

    protected void Shoot()
    {
        if (!isDefeated)
        {
            Vector3 position = transform.position; position.y -= 0.2F;
            Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

            newBullet.Parent = gameObject;
            newBullet.spriteImg = bulletImg;
            newBullet.bulletSpeed = 8.0f;
            newBullet.gameObject.tag = "Enemy";
            newBullet.Direction = newBullet.transform.right * (!facingRight ? -1.0F : 1.0F);
        }
    }

    protected void CheckWhereToFace()
    {
        if (transform.position.x - character.transform.position.x > 0)
            facingRight = false;
        else
            facingRight = true;

        if (((facingRight) && (localScaleX < 0)) || ((!facingRight) && (localScaleX > 0)))
            localScaleX *= -1;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

}
