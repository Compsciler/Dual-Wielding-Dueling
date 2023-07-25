using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public Transform shooter;
    void OnCollisionEnter(Collision collision)
    {
        Entity entity = collision.transform.GetComponent<Entity>();
        if(entity==shooter.GetComponentInParent<Entity>())
        {
            return;
        }
        if(entity != null)
        {
            entity.TakeDamage(5);
        }
        Destroy(gameObject);
    }
}
