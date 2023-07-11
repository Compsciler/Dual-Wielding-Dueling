using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGun : WeaponArm
{
    public GameObject ammo; 
    public float ammoSpeed;

    private Collider gc;

    public delegate IEnumerator CoroutineDelegate(GameObject projectile, float delay);
    private CoroutineDelegate destroyProjectileDelegate;

    protected override void Start()
    {
        base.Start();
        destroyProjectileDelegate = DestroyProjectile;
        gc = gunTip.parent.GetComponent<Collider>();
    }

    public override void Fire()
    {
        base.Fire();
        GameObject projectile = Instantiate(ammo);
        projectile.GetComponent<Projectile>().shooter=transform;
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), gc);
        projectile.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        projectile.transform.position = gunTip.transform.position;
        projectile.GetComponent<Rigidbody>().AddForce(gunTip.forward * ammoSpeed, ForceMode.Impulse);
        StartCoroutine(destroyProjectileDelegate(projectile, 5f));
    }
    private IEnumerator DestroyProjectile (GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(projectile);
    }
}
