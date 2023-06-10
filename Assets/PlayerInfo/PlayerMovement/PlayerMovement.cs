using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public float jumpForce;
    public float jumpTimer;
    public float airPenalty;

    public float drag;

    public Transform orientation;
    
    float horizontalInput;
    float verticalInput;

    Vector3 dir;

    Rigidbody rb;

    public float playerHeight;
    public KeyCode jumpKey;
    public LayerMask whatIsGround;

    bool grounded;
    bool canJump;
    // Start is called before the first frame update
    void Start()
    {
        
        rb=GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canJump = true;
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
            rb.AddForce(dir.normalized*moveSpeed*10f*airPenalty,ForceMode.Force);
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
        rb.AddForce(transform.up*jumpForce,ForceMode.Impulse);
    }

    private void reset()
    {
        canJump = true;
    }

    public bool isGrounded(){
        return grounded;
    }
}
