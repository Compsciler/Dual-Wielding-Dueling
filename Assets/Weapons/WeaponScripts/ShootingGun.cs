using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGun : WeaponArm
{
    public GameObject ammo; 
    public float ammoSpeed;

    public override void Fire()
    {
        base.Fire();
        GameObject projectile = Instantiate(ammo);
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), gunTip.parent.GetComponent<Collider>());
        projectile.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        projectile.transform.position = gunTip.transform.position;
        projectile.GetComponent<Rigidbody>().AddForce(gunTip.forward * ammoSpeed, ForceMode.Impulse);
        StartCoroutine("DestroyProjectile");
    }
    private IEnumerator DestroyProjectile (GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(projectile);
    }
}
