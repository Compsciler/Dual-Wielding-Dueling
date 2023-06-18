using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    [SerializeField] GameObject hitParticlesPrefab;

    public static Action<DummyTarget> OnDummyHit;  // Refactor later so hit and death are separate
    public static Action<DummyTarget> OnDummySpawned;

    void Start()
    {
        OnDummySpawned?.Invoke(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            TakeDamage();
            OnDummyHit?.Invoke(this);
            return;
        }

        if (collision.gameObject.TryGetComponent<Projectile>(out Projectile projectile))
        {
            TakeDamage();
            OnDummyHit?.Invoke(this);
            return;
        }
    }

    void TakeDamage()
    {
        Instantiate(hitParticlesPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
