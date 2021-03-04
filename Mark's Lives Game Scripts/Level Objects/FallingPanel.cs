using System.Collections;
using UnityEngine;

public class FallingPanel : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 initialPosition;
    private bool movePlatformBack;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (movePlatformBack)
        {
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, 20.0f * Time.deltaTime);
        }

        if (transform.position.y == initialPosition.y)
        {
            movePlatformBack = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player") && !movePlatformBack)
        {
            Invoke("DropPlatform", 0.5f);
        }
    }

    private void DropPlatform()
    {
        rb.isKinematic = false;
        Invoke("GetPlatformBack", 1.5f);
    }

    private void GetPlatformBack()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        movePlatformBack = true;
    }
}
