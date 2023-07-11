using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private WeaponArm weaponArm;
    private RectTransform rt;
    // Start is called before the first frame update
    void Start()
    {
        string lr = transform.name.Substring(0,4);
        if(!lr.Equals("Left"))
        {
            lr="Right";
        }
        rt = transform.GetComponent<RectTransform>();
        weaponArm=transform.parent.parent.parent.Find("CameraPosition").Find(lr+" Arm").GetComponentInChildren<WeaponArm>();
    }

    // Update is called once per frame
    void Update()
    {
        rt.anchoredPosition3D=weaponArm.GetCrosshair();
    }
}
