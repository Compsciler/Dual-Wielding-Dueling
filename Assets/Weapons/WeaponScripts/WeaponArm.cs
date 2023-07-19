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
    private GameObject pickUp;
    protected Transform player;

    protected Transform cam;
    private Camera camDisplay;
    private bool firing;
    protected bool left;

    [HideInInspector] protected float maxDistance;
    private float maxPickupDistance = 10f;

    public Action OnAmmoUpdated;
    public Action WeaponUpdated;

    void Awake()
    {
        pickUp = (GameObject) Resources.Load("Pickup", typeof(GameObject));
    }
    protected virtual void Start()
    {
        curBullets=maxBullets;
        timeLeft=reloadTime;
        shotTimeLeft=secsPerShot;
        firing = false;
        cam=Camera.main.transform;
        player=transform.parent.parent.parent;
        gunTip=transform.Find("GunTip");
        left = transform.parent.name.Equals("Left Arm") ? true : false;
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
        if (Input.GetMouseButtonDown(left?0:1)&&curBullets>0&&!firing&&shotTimeLeft<0) 
        {
            Fire();
        }
        else if (Input.GetMouseButtonUp(left?0:1)&&firing) 
        {
            Release();
        }
        else if(firing)
        {
            Hold();
        }
        if((left&&Input.GetKeyDown(KeyCode.Q))||(!left&&Input.GetKeyDown(KeyCode.E)))
        {
            Grab();
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
    public void Drop()
    {
        transform.SetParent(null);
        GameObject pickUpGun = Instantiate(pickUp,transform.position,Quaternion.identity);
        pickUpGun.GetComponent<PickUp>().item=gameObject;
    }
    public void Grab()
    {
        RaycastHit hit;
        if(Physics.Raycast(gunTip.position,gunTip.forward,out hit,maxPickupDistance))
        {
            PickUp target = hit.transform.GetComponent<PickUp>();
            if(target!=null)
            {
                target.Replace(transform);
                Drop();
                WeaponUpdated?.Invoke();
            }
        }
    }
}
