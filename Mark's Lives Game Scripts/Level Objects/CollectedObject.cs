using System.Collections;
using UnityEngine;

public class CollectedObject : MonoBehaviour
{
    private float centerY, maxY, minY, currentY, direction = 1.0f;

    private void Start()
    {
        centerY = transform.position.y;
        currentY = centerY;
        maxY = centerY + 0.1f;
        minY = centerY - 0.1f;
    }

    private void Update()
    {
        if (currentY >= maxY || currentY <= minY)
        {
            direction *= -1;
        }

        transform.position = new Vector2(
            transform.position.x,
            currentY
            );

        currentY += (0.005f * direction);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();

        if (character)
        {
            if (gameObject.tag.Equals("Bullet"))
            {
                character.Bullets++;
                Destroy(gameObject);
            }

            if (gameObject.tag.Equals("Heart"))
            {
                character.Lives++;
                Destroy(gameObject);
            }

            FindObjectOfType<AudioManager>().PlaySound("CollectItem");
        }
    }
}
