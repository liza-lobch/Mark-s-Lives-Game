using UnityEngine;
using System.Collections;

public abstract class Unit : MonoBehaviour
{
    protected Animator anim;
    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public virtual void ReceiveDamage()
    {
        Die();
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}