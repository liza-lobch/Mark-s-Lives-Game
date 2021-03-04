using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform targetToFollow;
    [SerializeField]
    private float minCameraPosX, maxCameraPosX;

    private void Update()
    {
        transform.position = new Vector3(
        Mathf.Clamp(targetToFollow.position.x + 3.5f, minCameraPosX, maxCameraPosX),
        Mathf.Clamp(targetToFollow.position.y + 0.3f, 0, 13.0f),
        //targetToFollow.position.y,
        transform.position.z
        );

        targetToFollow.position = new Vector2(
            Mathf.Clamp(targetToFollow.position.x, minCameraPosX - 7.0f, maxCameraPosX + 7.0f),
            targetToFollow.position.y
            );
    }
}
