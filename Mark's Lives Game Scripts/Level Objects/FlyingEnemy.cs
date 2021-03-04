using System.Collections;
using UnityEngine;

public class FlyingEnemy : MovingEnemy
{
    [SerializeField]
    protected float frequency = 5.0f;

    [SerializeField]
    protected float extremePosY = 0.5f;

    protected Vector3 pos;

    protected override void Start()
    {
        pos = transform.position;
        localScale = transform.localScale;
        dirX = -1.0f;
        centerPos = transform.position.x;
    }

    protected override void Update()
    {
        if (isMovingBetweenIndicatedLength)
        {
            MoveBetweenIndicatedLength();
        }

        if (facingRight)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
    }

    protected override void LateUpdate()
    {
        CheckWhereToFace();
    }

    protected void MoveRight()
    {
        pos += transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * extremePosY;
    }

    protected void MoveLeft()
    {
        pos -= transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * extremePosY;
    }

    protected override void FixedUpdate() { }

    public override void ReceiveDamage()
    {
        gameObject.tag = "Untagged";
        Destroy(gameObject.transform.GetChild(0).gameObject);
        isDefeated = true;
        anim.SetBool("isDefeated", true);
    }
}
