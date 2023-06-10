using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    [SerializeField] GameObject hitParticlesPrefab;

    public static Action OnDummyHit;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            TakeDamage();
            OnDummyHit?.Invoke();
        }
    }

    void TakeDamage()
    {
        Instantiate(hitParticlesPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
