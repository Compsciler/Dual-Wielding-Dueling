using UnityEngine;
using System;

public class GrapplingGun : MonoBehaviour {

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private float maxDistance = 1000f;
    public float lanch;
    private bool grappling;
    private SpringJoint joint;

    void Awake() {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;
        grappling = false;
    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            Pulse();
        }
        if (Input.GetMouseButtonDown(0)) {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0)) {
            StopGrapple();
        }
    }

    //Called after Update
    void LateUpdate() {
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable)) {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.25f;
            joint.minDistance = 0f;

            //Adjust these values to fit your game.
            joint.spring = 4.5f;
            joint.damper = 1f;
            joint.massScale = 4.5f;
            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
            Vector3 launch = CalculateJumpVelocity(currentGrapplePosition,grapplePoint);
            player.GetComponent<Rigidbody>().velocity+=launch;
            grappling=true;
        }
    }

    void Pulse(){
            Vector3 v = camera.forward;
            v/= - v.magnitude;
            player.GetComponent<Rigidbody>().AddForce(v*lanch,ForceMode.Impulse);
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple() {
        lr.positionCount = 0;
        Destroy(joint);
        grappling=false;
    }

    private Vector3 currentGrapplePosition;
    
    void DrawRope() {
        //If not grappling, don't draw rope
        if (!grappling) return;
        currentGrapplePosition = gunTip.position;
        lr.SetPosition(0, currentGrapplePosition);
        lr.SetPosition(1, grapplePoint);
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);
        float trajectoryHeight = displacementY+10f;
        if(displacementY<0)
        {
            trajectoryHeight=0;
        }
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) 
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        if(displacementY<0)
        {
            return velocityXZ;
        }
        return velocityXZ + velocityY;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}