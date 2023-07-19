using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyType : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHP;
    private float curHP;
    public float moveSpeed;

    public float jumpForce;
    public float airPenalty;

    public float drag;
    void Start()
    {
        curHP=maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {

    }

    public virtual void TakeDamage(float dmg)
    {
        curHP-=dmg;
        if(curHP<=0)
        {
            Die();
        }
    }

    public float getHP()
    {
        return curHP;
    }
}
