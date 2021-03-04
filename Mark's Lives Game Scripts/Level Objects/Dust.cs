using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    private GameObject dustCloud;

    private void Start()
    {
        dustCloud = Resources.Load<GameObject>("DustCloud");
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Ground"))
        {
            Destroy(Instantiate(dustCloud, transform.position, dustCloud.transform.rotation), 1.0f);
        }
    }
}
