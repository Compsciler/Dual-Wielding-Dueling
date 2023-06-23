using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] WeaponArm weaponArm;
    
    [SerializeField] TMP_Text pulseGunAmmoText;
    [SerializeField] Image pulseGunReloadImage;

    void Awake()
    {
        weaponArm.OnAmmoUpdated += UpdateAmmoDisplay;
    }

    void OnDestroy()
    {
        weaponArm.OnAmmoUpdated -= UpdateAmmoDisplay;
    }

    private void UpdateAmmoDisplay()
    {
        pulseGunAmmoText.text = weaponArm.CurBullets.ToString();
        if (weaponArm.CurBullets == 0)
        {
            pulseGunReloadImage.fillAmount = weaponArm.TimeLeft / weaponArm.reloadTime;
        }
    }
}
