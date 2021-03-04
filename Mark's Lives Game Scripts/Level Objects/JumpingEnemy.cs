using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : MovingEnemy
{
    [SerializeField]
    private float bigForce = 450.0f, smallForce = 350.0f;

    protected override void Update()
    {
        if (!isDefeated)
        {
            if (isMovingBetweenIndicatedLength)
            {
                MoveBetweenIndicatedLength();
            }
            SetAnimationState();
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

            switch (collider.tag)
            {
                case "BigEnemyObstacle":
                    rb.AddForce(Vector2.up * bigForce);
                    break;

                case "SmallEnemyObstacle":
                    rb.AddForce(Vector2.up * smallForce);
                    break;
                default:
                    dirX = -dirX;
                    break;
            }
        }
    }

    private void SetAnimationState()
    {
        if (Mathf.Abs(rb.velocity.y) > 0)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isJumping", false);
        }
    }

    public override void ReceiveDamage()
    {
        if (Mathf.Abs(rb.velocity.y) > 0)
        {
            transform.position = new Vector3(
            transform.position.x,
            Mathf.Lerp(3.4f, transform.position.y, 0.1f),
            transform.position.z
            );
        }
        gameObject.tag = "Untagged";
        Destroy(gameObject.transform.GetChild(0).gameObject);
        Destroy(gameObject.transform.GetChild(1).gameObject);
        Destroy(GetComponent<BoxCollider2D>());
        isDefeated = true;
        anim.SetBool("isDefeated", true);
        rb.velocity = new Vector2(0, 0);
        rb.isKinematic = true;
    }
}
