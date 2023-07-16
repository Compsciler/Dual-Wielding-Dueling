using UnityEngine;
using System;

public class PulseGun : WeaponArm {

    public float launch;
    public float maxDist;

    protected override void Start()
    {
        base.maxDistance=maxDist;
        base.Start();
    }

    public override void Fire(){
        Vector3 v = transform.forward;
        v/= - v.magnitude;
        player.GetComponent<Rigidbody>().AddForce(v*launch,ForceMode.Impulse);
        RaycastHit hit;
        if(Physics.Raycast(gunTip.position,gunTip.forward, out hit, maxDistance))
        {
            Entity target = hit.transform.GetComponent<Entity>();
            if(target!=null)
            {
                target.rb.AddForce(v*launch*-1,ForceMode.Impulse);
            }
        }
        base.Fire();
    }
}