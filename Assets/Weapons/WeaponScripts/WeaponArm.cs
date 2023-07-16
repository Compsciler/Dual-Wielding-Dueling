using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public abstract class WeaponArm: MonoBehaviour
{
    // Start is called before the first frame update
    public string weaponName;
    public Sprite weaponIcon;
    public int maxBullets;
    protected int curBullets;
    public int CurBullets => curBullets;
    public float reloadTime;
    public float secsPerShot;
    protected float timeLeft;
    public float TimeLeft => timeLeft;
    private float shotTimeLeft;
    protected Transform gunTip;

    protected Transform player;

    protected Transform cam;
    private Camera camDisplay;
    private bool firing;
    protected int arm;

    [HideInInspector] protected float maxDistance;

    public Action OnAmmoUpdated;

    protected virtual void Start()
    {
        curBullets=maxBullets;
        timeLeft=reloadTime;
        shotTimeLeft=secsPerShot;
        firing = false;
        cam=Camera.main.transform;
        player=transform.parent.parent.parent;
        gunTip=transform.Find("GunTip");
        arm = transform.parent.name.Equals("Right Arm") ? 1 : 0;
        camDisplay=cam.GetComponent<Camera>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(!firing)
        {
            timeLeft-=Time.deltaTime;
            shotTimeLeft-=Time.deltaTime;
        }
        else
        {
            timeLeft=reloadTime;
            shotTimeLeft=secsPerShot;
        }
        if(timeLeft<0)
        {
            curBullets=maxBullets;
        }

        OnAmmoUpdated?.Invoke();
    }

    protected virtual void LateUpdate()
    {
        if (Input.GetMouseButtonDown(arm)&&curBullets>0&&!firing&&shotTimeLeft<0) 
        {
            Fire();
        }
        else if (Input.GetMouseButtonUp(arm)&&firing) 
        {
            Release();
        }
        if(firing)
        {
            Hold();
        }
    }
    public virtual void Fire()
    {
        firing=true;
        if(curBullets>0)
        {
            curBullets--;
        }
        timeLeft=reloadTime;
        shotTimeLeft=secsPerShot;
    }
    public virtual void Hold()
    {
    }
    public virtual void Release()
    {
        firing=false;
    }
    public virtual Vector3? GetTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(gunTip.position, gunTip.forward, out hit, maxDistance))
        {
            return hit.point;
        }
        return null;
    }
}
