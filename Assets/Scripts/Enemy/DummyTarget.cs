using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    [SerializeField] GameObject hitParticlesPrefab;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        Instantiate(hitParticlesPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
