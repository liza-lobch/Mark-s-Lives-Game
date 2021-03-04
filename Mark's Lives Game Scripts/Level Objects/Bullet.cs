using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private GameObject parent;
    public GameObject Parent { set { parent = value; } get { return parent; } }

    protected float speed;
    protected Vector3 direction;
    public Vector3 Direction { set { direction = value; } }

    public Color Color
    {
        set { sprite.color = value; }
    }

    public Sprite spriteImg
    {
        set { sprite.sprite = value; }
    }

    public float bulletSpeed
    {
        set { speed = value; }
    }


    protected SpriteRenderer sprite;

    protected void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        Destroy(gameObject, 0.8F);
    }

    protected void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject item = collider.gameObject;

        if (item != Parent && item.tag != "TrashBin")
        {
            Destroy(gameObject);
        }
    }
}
