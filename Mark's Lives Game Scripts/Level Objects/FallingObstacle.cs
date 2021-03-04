using System.Collections;
using UnityEngine;

public class FallingObstacle : Obstacle
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();

        if (character)
        {
            rb.isKinematic = false;
        }
    }

    protected void OnCollisionEnter2D(Collision2D col)
    {
        rb.AddForce(new Vector2(Random.Range(-200f, 200f), 200f));
        Invoke("Die", 0.2f);
    }
}
