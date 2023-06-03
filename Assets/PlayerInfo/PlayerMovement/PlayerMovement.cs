using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public Transform orientation;
    
    float hI;
    float vI;

    Vector3 dir;

    Rigidbody rb;

    public float playerHeight;
    public KeyCode jK;
    public LayerMask whatIsGround;
    public float jF;
    public float jC;
    public float air;
    bool grounded;
    bool cJ;
    public float drag;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cJ = true;
    }

    // Update is called once per frame

    private void MyInput()
    {
        hI = Input.GetAxisRaw("Horizontal");
        vI = Input.GetAxisRaw("Vertical");

        if(grounded && Input.GetKey(jK) && cJ)
        {
            cJ = false;
            Jump();
            Invoke(nameof(reset), jC);
        }
    }

    private void MovePlayer()
    {
        dir = orientation.forward * vI + orientation.right * hI;
        if(grounded)
        {
            rb.AddForce(dir.normalized*moveSpeed*10f,ForceMode.Force);
        }
        else
        {
            rb.AddForce(dir.normalized*moveSpeed*10f*air,ForceMode.Force);
        }
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

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
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        rb.AddForce(transform.up*jF,ForceMode.Impulse);
    }

    private void reset()
    {
        cJ = true;
    }

    public bool isGrounded(){
        return grounded;
    }
}
