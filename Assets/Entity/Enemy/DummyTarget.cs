using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTarget : MonoBehaviour, Entity
{

    public static Action<DummyTarget> OnDummyHit;  // Refactor later so hit and death are separate
    public static Action<DummyTarget> OnDummySpawned;
    private Rigidbody rigidbod;

    public Rigidbody rb
    {
        get => rigidbod;
    }

    void Start()
    {
        rigidbod=transform.GetComponent<Rigidbody>();
        OnDummySpawned?.Invoke(this);
    }

    public void TakeDamage(float dmg)
    {
        Destroy(gameObject);
    }
}
