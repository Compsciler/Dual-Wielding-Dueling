using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, Entity
{
    // Start is called before the first frame update
    public GameObject item;

    public float spinPeriod;
    public float upForce;
    public float drag;

    public LayerMask ground;
    public Rigidbody rb
    {
        get => transform.GetComponent<Rigidbody>();
    }

    private float hp=1;

    Material leftOutlineMaterial;
    Material rightOutlineMaterial;

    [SerializeField] float outlineThickness = 1f;

    void Start()
    {
        rb.drag=drag;
        item.SetActive(false);
        GameObject go = Instantiate(item);
        go.SetActive(true);
        Destroy(go.GetComponent<WeaponArm>());
        Destroy(go.transform.Find("GunTip").gameObject);
        go.transform.SetParent(transform);
        go.transform.GetComponent<Collider>().enabled=true;
        go.transform.localPosition=Vector3.zero;
        go.transform.rotation=Quaternion.identity;

        Material[] materials = go.GetComponent<Renderer>().materials;
        leftOutlineMaterial = materials[1];
        rightOutlineMaterial = materials[2];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Motion();
    }

    void OnEnable()
    {
        WeaponArm.OnPickUpEnter += HandlePickUpEnter;
        WeaponArm.OnPickUpExit += HandlePickUpExit;
    }

    void OnDisable()
    {
        WeaponArm.OnPickUpEnter -= HandlePickUpEnter;
        WeaponArm.OnPickUpExit -= HandlePickUpExit;
    }

    private void HandlePickUpEnter(PickUp target, bool left)
    {
        if (target != this) { return; }

        if (left)
        {
            leftOutlineMaterial.SetFloat("_OutlineThickness", outlineThickness);
        }
        else
        {
            rightOutlineMaterial.SetFloat("_OutlineThickness", outlineThickness);
        }
    }

    private void HandlePickUpExit(PickUp target, bool left)
    {
        if (target != this) { return; }

        if (left)
        {
            leftOutlineMaterial.SetFloat("_OutlineThickness", 0f);
        }
        else
        {
            rightOutlineMaterial.SetFloat("_OutlineThickness", 0f);
        }
    }

    public void TakeDamage(float dmg)
    {
        hp-=dmg;
        if(hp<=0)
        {
            Destroy(gameObject);
        }
    }

    public void Motion()
    {
        transform.Rotate(new Vector3(0,Time.deltaTime/spinPeriod*360,0));
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity,layerMask: ground))
        {
            float force = 0;
            force = Mathf.Pow(Mathf.Abs(hit.point.y - transform.position.y),-0.5f);
            rb.AddForceAtPosition(transform.up * force * upForce, transform.position, ForceMode.Acceleration);
        }
    }

    public void Replace(Transform old)
    {
        Destroy(gameObject);
        GameObject go=Instantiate(item);
        go.SetActive(true);
        go.transform.SetParent(old.parent);
        go.transform.localPosition = old.localPosition;
        go.transform.localRotation = old.localRotation;
    }
}
