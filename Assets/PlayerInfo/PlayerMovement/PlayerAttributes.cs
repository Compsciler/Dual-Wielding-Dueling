using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour, Entity
{
    private float moveSpeed;    
    private float jumpForce;
    public float jumpTimer;
    private float airModifier;

    private float drag;
    protected BodyType body;

    public Transform orientation;
    
    float horizontalInput;
    float verticalInput;

    Vector3 dir;

    public Rigidbody rb
    {
        get => GetComponent<Rigidbody>();
    }

    private float playerHeight;
    public KeyCode jumpKey;
    public KeyCode leftDrop;
    public KeyCode rightDrop;
    public LayerMask groundLayer;
    public GameObject pickUp;
    bool grounded;
    bool canJump;
    // Start is called before the first frame update
    void Start()
    {
        rb.freezeRotation = true;
        canJump = true;
        Transform bodyTransform=transform.Find("Body");
        body = bodyTransform.GetComponentInChildren<BodyType>();
        moveSpeed = body.moveSpeed;
        jumpForce = body.jumpForce;
        airModifier = (1-body.airPenalty);
        drag = body.drag;
        playerHeight = bodyTransform.GetChild(0).GetComponent<CapsuleCollider>().height*bodyTransform.GetChild(0).transform.localScale.y;
    }

    // Update is called once per frame

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(grounded && Input.GetKey(jumpKey) && canJump)
        {
            canJump = false;
            Jump();
            Invoke(nameof(reset), jumpTimer);
        }
        if(Input.GetKey(leftDrop))
        {

        }
        if(Input.GetKey(rightDrop))
        {

        }
    }

    private void MovePlayer()
    {
        dir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if(grounded)
        {
            rb.AddForce(dir.normalized*moveSpeed*10f,ForceMode.Force);
        }
        else
        {
            rb.AddForce(dir.normalized*moveSpeed*10f*airModifier,ForceMode.Force);
        }
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.01f, groundLayer);

        MyInput();

        if(grounded)
        {
            rb.drag=drag;
        }
        else
        {
            rb.drag=0;
        }
        SpeedControl();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void SpeedControl()
    {
        Vector3 flat = new Vector3(rb.velocity.x,0f,rb.velocity.z);

        if(flat.magnitude > moveSpeed && grounded)
        {
            Vector3 l = flat.normalized*moveSpeed;
            rb.velocity = new Vector3(l.x,rb.velocity.y,l.z);
        }
        if(dir.Equals(Vector3.zero) && grounded)
        {
            rb.velocity=Vector3.zero;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        rb.AddForce(transform.up*jumpForce,ForceMode.Impulse);
    }

    private void reset()
    {
        canJump = true;
    }

    public bool isGrounded()
    {
        return grounded;
    }
    public void TakeDamage(float dmg)
    {
        body.TakeDamage(dmg);
        if(body.getHP()<=0)
        {
            Die();
        }
    }

    public void DropGun(bool left)
    {
       GameObject go = transform.Find("CameraPosition").Find((left ? "Left" : "Right")+" Arm").GetChild(0).gameObject;
       go.transform.SetParent(null);
       GameObject pickUpGun = Instantiate(pickUp,transform.position,transform.rotation);
       pickUpGun.GetComponent<PickUp>().item=go;
    }

    public void Die()
    {
        DropGun(true);
        DropGun(false);
    }

    public void GrabGun(bool left)
    {
        
    }
}
