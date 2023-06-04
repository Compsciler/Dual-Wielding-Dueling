using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CrosshairData
{
        public Sprite CrosshairSprite;
        public int CrosshairSize;
        public Color CrosshairColor;
}

public abstract class WeaponArm: MonoBehaviour
{
    // Start is called before the first frame update
    public string weaponName;
    public Sprite weaponIcon;
    public int maxBullets;
    private int curBullets;
    public float reloadTime;
    public float secsPerShot;
    private float timeLeft;
    private float shotTimeLeft;
    public CrosshairData defCrosshair;
    public Transform gunTip, player;

    protected Transform cam;
    private bool firing;
    public int arm;
    
    void Start()
    {
        curBullets=maxBullets;
        timeLeft=reloadTime;
        shotTimeLeft=secsPerShot;
        firing = false;
        cam=Camera.main.transform;
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
    }

    void LateUpdate()
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
}
