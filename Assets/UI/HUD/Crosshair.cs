using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private WeaponArm weaponArm;
    private RectTransform rt;
    private Image img;
    // Start is called before the first frame update
    void Start()
    {
        string lr = transform.name.Substring(0,4);
        if(!lr.Equals("Left"))
        {
            lr="Right";
        }
        rt = transform.GetComponent<RectTransform>();
        img = transform.GetComponent<Image>();
        weaponArm=transform.parent.parent.parent.Find("CameraPosition").Find(lr+" Arm").GetComponentInChildren<WeaponArm>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3? target=weaponArm.GetTarget();
        if(target.HasValue && Physics.Raycast(Camera.main.transform.position, (target.Value-Camera.main.transform.position).normalized))
        {
            img.enabled=true;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.Value);
            Vector3 uiPos = new Vector3(screenPos.x - Screen.width / 2, screenPos.y - Screen.height / 2, screenPos.z);
            rt.anchoredPosition3D=uiPos;
        }
        else
        {
            img.enabled=false;
        }
    }
}
