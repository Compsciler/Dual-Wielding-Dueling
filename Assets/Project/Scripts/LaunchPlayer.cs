using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPlayer : MonoBehaviour
{
    [SerializeField] float launchForceMultiplier = 1f;
    [SerializeField] float distanceAtMaxForce = 1f;  // Maximum force is applied at this distance and closer
    [Range(0.1f, 10f)] [SerializeField] float forceDistanceInverseExponent = 2f;
    [SerializeField] float mass = 1f;

    Rigidbody rb;

    void Awake()
    {
        if (TryGetComponent<Rigidbody>(out rb))
        {
            mass = rb.mass;
        }
    }

    public Vector3 GetLaunchVelocity(Vector3 launchPosOrigin)
    {
        Vector3 launchDirection = (transform.position - launchPosOrigin).normalized;
        float distance = Vector3.Distance(launchPosOrigin, transform.position);  // Could use sqrMagnitude instead
        distance = Mathf.Max(distance, distanceAtMaxForce);
        float forceMagnitude = launchForceMultiplier / Mathf.Pow(distance, forceDistanceInverseExponent);
        Vector3 force = (transform.position - launchPosOrigin) * forceMagnitude;
        Vector3 velocity = force / mass;
        Debug.Log($"launchHit: {launchPosOrigin} | transform.position: {transform.position} | launchDirection: {launchDirection} | distance: {distance} | launchSpeed: {velocity.magnitude}");
        return velocity;
    }
}
