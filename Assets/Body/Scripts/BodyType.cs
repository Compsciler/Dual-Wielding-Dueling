using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyType : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHP;
    private float curHP;
    public float speed;
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

    public virtual void takeDamage(float dmg)
    {
        curHP-=dmg;
        if(curHP<=0)
        {
            Die();
        }
    }
}
