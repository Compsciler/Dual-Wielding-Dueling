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
    public string WeaponName;
    public Sprite WeaponIcon;
    public int maxBullets;
    private int curBullets;
    public float reloadTime;
    public float SecsPerShot;
    public float timeLeft;
    public AmmoController ammo;
    public CrosshairData defCrosshair;
    public Transform gunTip, camera, player;
    public bool firing;
    public int arm;
    
    void Start()
    {
        curBullets=maxBullets;
        timeLeft=reloadTime;
        firing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!firing)
        {
            timeLeft-=Time.deltaTime;
        }
        else
        {
            timeLeft=reloadTime;
        }
        if(timeLeft<0)
        {
            curBullets=maxBullets;
        }
    }

    void LateUpdate()
    {
         if (Input.GetMouseButtonDown(arm)&&curBullets>0&&!firing) {
            Fire();
        }
        else if (Input.GetMouseButtonUp(arm)&&firing) {
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
    }
    public virtual void Hold()
    {}
    public virtual void Release()
    {
        firing=false;
    }
}
