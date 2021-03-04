using System.Collections;
using UnityEngine;

public class Enemy : Unit
{
    protected bool isDefeated;
    protected virtual void Start() { }
    protected virtual void Update() { }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
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

    public override void ReceiveDamage()
    {
        gameObject.tag = "Untagged";
        Destroy(gameObject.transform.GetChild(0).gameObject);
        isDefeated = true;
        anim.SetBool("isDefeated", true);
    }
}
