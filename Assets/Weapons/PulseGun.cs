using UnityEngine;
using System;

public class PulseGun : WeaponArm {

    public float lanch;


    public override void Fire(){
        Vector3 v = camera.forward;
        v/= - v.magnitude;
        player.GetComponent<Rigidbody>().AddForce(v*lanch,ForceMode.Impulse);
        base.Fire();
    }

}