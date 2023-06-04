using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class PulseGun : WeaponArm {

    public float launch;

    [SerializeField] TMP_Text pulseGunAmmoText;
    [SerializeField] Image pulseGunReloadImage;


    public override void Fire(){
        Vector3 v = cam.forward;
        v/= - v.magnitude;
        player.GetComponent<Rigidbody>().AddForce(v*launch,ForceMode.Impulse);
        base.Fire();
    }

    protected override void Update()
    {
        base.Update();
        
        pulseGunAmmoText.text = curBullets.ToString();
        if (curBullets == 0)
        {
            pulseGunReloadImage.fillAmount = timeLeft / reloadTime;
        }
    }
}