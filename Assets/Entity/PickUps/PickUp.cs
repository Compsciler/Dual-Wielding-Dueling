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
    void Start()
    {
        rb.drag=drag;
        GameObject go = Instantiate(item);
        Destroy(go.GetComponent<WeaponArm>());
        Destroy(go.transform.Find("GunTip").gameObject);
        go.transform.SetParent(transform);
        go.transform.GetComponent<Collider>().enabled=true;
        go.transform.position=transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Motion();
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
}
