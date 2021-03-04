using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingShootingEnemy : ShootingEnemy
{
    private bool isHidding = true, playerIsHitted, playerIsTouching;

    protected override void Start()
    {
        character = FindObjectOfType<Character>();
        localScaleX = transform.localScale.x;

        Physics2D.IgnoreLayerCollision(8, 13, true);
        InvokeRepeating("StartShootingAndHiding", rate, rate);
    }

    protected override void Update()
    {
        if (!isDefeated)
        {
            if (playerIsTouching && !playerIsHitted && !isHidding)
            {
                playerIsHitted = true;
                Character player = FindObjectOfType<Character>();
                player.ReceiveDamage();
            }
        }
    }

    protected override void LateUpdate()
    {
        if (!isDefeated)
        {
            CheckWhereToFace();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (!isDefeated)
        {
            if (col.gameObject.tag.Equals("Player"))
            {
                playerIsTouching = true;
            }

            Bullet bullet = col.GetComponent<Bullet>();

            if (!isHidding && bullet && bullet.Parent != gameObject)
            {
                isDefeated = true;
                ReceiveDamage();
            }
        }
    }

    protected void OnTriggerExit2D(Collider2D col)
    {
        if (!isDefeated)
        {
            if (col.gameObject.tag.Equals("Player"))
            {
                playerIsTouching = false;
            }
        }
    }

    private void StartShootingAndHiding()
    {
        if (!isDefeated)
        {
            StartCoroutine("ShootThenHide");
        }
    }

    private IEnumerator ShootThenHide()
    {
        isHidding = false;
        Physics2D.IgnoreLayerCollision(8, 13, false);
        anim.SetBool("isShooting", true);
        Shoot();
        yield return new WaitForSeconds(1.5f);
        isHidding = true;
        Physics2D.IgnoreLayerCollision(8, 13, true);
        anim.SetBool("isShooting", false);
        playerIsHitted = false;
    }

    public override void ReceiveDamage()
    {
        isDefeated = true;
        anim.SetBool("isDefeated", true);
    }
}
