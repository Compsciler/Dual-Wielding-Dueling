using UnityEngine;


public class PulseGun : WeaponArm {

    public float launch;
    ParticleSystem pulseParticles;

    protected override void Start()
    {
        base.Start();
        {
            pulseParticles=gunTip.Find("ShotParticles").GetComponentInChildren<ParticleSystem>();
        }
    }

    public override void Fire(){
        Vector3 v = cam.forward;
        v/= - v.magnitude;
        player.GetComponent<Rigidbody>().AddForce(v*launch,ForceMode.Impulse);
        base.Fire();
        pulseParticles.Play();
    }
}