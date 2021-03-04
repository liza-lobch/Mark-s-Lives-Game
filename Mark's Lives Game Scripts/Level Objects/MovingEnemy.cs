using System.Collections;
using UnityEngine;
using System.Linq;

public class MovingEnemy : Enemy
{
    [SerializeField]
    protected bool isMovingBetweenIndicatedLength;
    [SerializeField]
    protected float MovingDistance = 5.0f;

    [SerializeField]
    protected float moveSpeed = 2.5f;

    protected float dirX;
    protected float centerPos;

    protected Rigidbody2D rb;
    protected bool facingRight = false;
    protected Vector3 localScale;

    protected override void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        dirX = -1.0f;
        centerPos = transform.position.x;
        anim.SetBool("isRunning", true);
    }

    protected override void Update()
    {
        if (isMovingBetweenIndicatedLength && !isDefeated)
        {
            MoveBetweenIndicatedLength();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (!isDefeated)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }
    }

    protected virtual void LateUpdate()
    {
        if (!isDefeated)
        {
            CheckWhereToFace();
        }
    }

    protected void CheckWhereToFace()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;
    }

    protected void MoveBetweenIndicatedLength()
    {
        if (transform.position.x < centerPos - MovingDistance)
        {
            dirX = 1.0f;
        }
        else if (transform.position.x > centerPos + MovingDistance)
        {
            dirX = -1.0f;
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isDefeated)
        {
            Bullet bullet = collider.GetComponent<Bullet>();

            if (bullet)
            {
                ReceiveDamage();
            }

            dirX = -dirX;
        }
    }

    public override void ReceiveDamage()
    {
        base.ReceiveDamage();
        rb.isKinematic = true;
        rb.velocity = new Vector2(0, 0);
        Destroy(GetComponent<BoxCollider2D>());
    }
}
