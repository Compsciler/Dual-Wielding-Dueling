using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    private WeaponArm weaponArm;
    private string lr;
    
    [SerializeField] TMP_Text gunAmmoText;
    [SerializeField] Image gunReloadImage;

    void Awake()
    {
        lr = "Right";
        if(transform.name.Equals("Left Gun Reload"))
        {
            lr = "Left";
        }
        weaponArm=transform.parent.parent.Find("CameraPosition").Find(lr+" Arm").GetComponentInChildren<WeaponArm>();
        weaponArm.OnAmmoUpdated += UpdateAmmoDisplay;
        weaponArm.WeaponUpdated += SetArm;
    }

    void OnDestroy()
    {
        weaponArm.OnAmmoUpdated -= UpdateAmmoDisplay;
    }

    private void UpdateAmmoDisplay()
    {
        gunAmmoText.text = weaponArm.CurBullets.ToString();
        if (weaponArm.CurBullets == 0)
        {
            gunReloadImage.fillAmount = weaponArm.TimeLeft / weaponArm.reloadTime;
        }
    }

    private void SetArm()
    {
        weaponArm=transform.parent.parent.Find("CameraPosition").Find(lr+" Arm").GetComponentInChildren<WeaponArm>();
        weaponArm.OnAmmoUpdated += UpdateAmmoDisplay;
        weaponArm.WeaponUpdated += SetArm;
    }
}
