using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponArm: MonoBehaviour
{
    // Start is called before the first frame update
    public string weaponName;
    public int maxBullets;
    protected int curBullets;
    public float reloadTime;
    public float secsPerShot;
    protected float timeLeft;
    protected float shotTimeLeft;
    protected Transform gunTip;
    protected Transform player;

    protected Transform cam;
    protected bool firing;
    int arm;

    TMP_Text gunAmmoText;
    Image gunReloadImage;

    public Transform throwable;
    
    protected virtual void Start()
    {
        curBullets=maxBullets;
        timeLeft=reloadTime;
        shotTimeLeft=secsPerShot;
        firing = false;
        cam=Camera.main.transform;
        gunTip=transform.Find("GunTip");
        player=transform.parent.parent.parent;
        if(transform.parent.name.Equals("LeftArm"))
        {
            arm=0;
        }
        else
        {
            arm=1;
        }
        Transform display=transform.parent.parent.parent.Find("Display");
        if(arm==0)
        {
            gunAmmoText=display.Find("LeftReload").Find("LeftAmmo").GetComponent<TMP_Text>();
            gunReloadImage=display.Find("LeftReload").GetComponent<Image>();
        }
        else
        {
            gunAmmoText=display.Find("RightReload").Find("RightAmmo").GetComponent<TMP_Text>();
            gunReloadImage=display.Find("RightReload").GetComponent<Image>();
        }
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
        gunAmmoText.text = curBullets.ToString();
        if (curBullets == 0)
        {
            gunReloadImage.fillAmount = timeLeft / reloadTime;
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
