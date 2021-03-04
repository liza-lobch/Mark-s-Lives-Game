using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();

        if (character)
        {
            character.ReceiveDamage();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}