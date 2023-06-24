using UnityEngine;
using System;

public class PulseGun : WeaponArm {

    public float launch;

    [SerializeField] ParticleSystem pulseParticles;


    public override void Fire(){
        Vector3 v = cam.forward;
        v/= - v.magnitude;
        player.GetComponent<Rigidbody>().AddForce(v*launch,ForceMode.Impulse);
        base.Fire();
        pulseParticles.Play();
    }
}