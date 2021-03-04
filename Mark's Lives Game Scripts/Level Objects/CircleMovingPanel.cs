using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovingPanel : MovingPanel
{
    [SerializeField]
    protected float OvalXProportion = 1, OvalYProportion = 1;
    [SerializeField]
    protected bool isClockwise;

    protected float posX, posY, angle = 0;

    protected override void Update()
    {
        posX = centerPos.x + Mathf.Cos(angle) * (distanceRadius * OvalXProportion);
        posY = centerPos.y + Mathf.Sin(angle) * (distanceRadius * OvalYProportion);
        transform.position = new Vector2(posX, posY);
        if (isClockwise)
        {
            angle = angle - Time.deltaTime * moveSpeed;
        }
        else
        {
            angle = angle + Time.deltaTime * moveSpeed;
        }

        if (angle >= 2 * Mathf.PI || angle <= -2 * Mathf.PI)
        {
            angle = 0;
        }
    }
}
