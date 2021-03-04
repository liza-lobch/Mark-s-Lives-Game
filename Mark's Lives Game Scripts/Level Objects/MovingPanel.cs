using System.Collections;
using UnityEngine;

public class MovingPanel : MonoBehaviour
{
    [SerializeField]
    protected float distanceRadius = 4.0f, moveSpeed = 3.0f;
    [SerializeField]
    bool byY;

    protected Vector3 centerPos;
    protected bool moveRight = true;

    protected virtual void Start()
    {
        centerPos = transform.position;
    }

    protected virtual void Update()
    {
        if (!byY)
        {
            if (transform.position.x > centerPos.x + distanceRadius)
            {
                moveRight = false;
            }
            if (transform.position.x < centerPos.x - distanceRadius)
            {
                moveRight = true;
            }

            if (moveRight)
            {
                transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            }
        }
        else
        {
            if (transform.position.y > centerPos.y + distanceRadius)
            {
                moveRight = false;
            }
            if (transform.position.y < centerPos.y - distanceRadius)
            {
                moveRight = true;
            }

            if (moveRight)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
            }
        }
    }
}
