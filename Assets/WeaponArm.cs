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
    public int curBullets;
    public float reloadTime;
    public float shotsPS;
    public float timeLeft;
    public Transform muzzle;
    public AmmoController ammo;
    public CrosshairData defCrosshair;
    public bool firing;
    
    void Start()
    {
        curBullets=maxBullets;
        timeLeft=reloadTime;
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public virtual void Release()
    {
        firing=false;
    }
}
