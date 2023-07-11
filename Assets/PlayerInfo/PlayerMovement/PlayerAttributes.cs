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
    protected Transform body;

    public Transform orientation;
    
    float horizontalInput;
    float verticalInput;

    Vector3 dir;

    Rigidbody rb;

    private float playerHeight;
    public KeyCode jumpKey;
    public LayerMask groundLayer;

    bool grounded;
    bool canJump;
    // Start is called before the first frame update
    void Start()
    {
        
        rb=GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canJump = true;
        body=transform.Find("Body");
        BodyType bt = body.GetComponentInChildren<BodyType>();
        moveSpeed = bt.moveSpeed;
        jumpForce = bt.jumpForce;
        airModifier = (1-bt.airPenalty);
        drag = bt.drag;
        playerHeight = body.GetChild(0).GetComponent<CapsuleCollider>().height*body.GetChild(0).transform.localScale.y;
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
        body.GetComponentInChildren<BodyType>().TakeDamage(dmg);
    }
}
