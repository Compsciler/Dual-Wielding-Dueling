using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGun : WeaponArm
{
    public GameObject ammo; 
    public float ammoSpeed;

    public delegate IEnumerator CoroutineDelegate(GameObject projectile, float delay);
    private CoroutineDelegate destroyProjectileDelegate;

    void Awake()
    {
        destroyProjectileDelegate = DestroyProjectile;
    }

    public override void Fire()
    {
        base.Fire();
        GameObject projectile = Instantiate(ammo);
        projectile.GetComponent<Projectile>().shooter=transform;
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), gunTip.parent.GetComponent<Collider>());
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
