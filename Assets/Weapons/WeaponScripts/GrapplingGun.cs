using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class GrapplingGun : WeaponArm {

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public float maxDistance;
    private SpringJoint joint;

    [SerializeField] float spring;
    [SerializeField] float damper;
    [SerializeField] float massScale;

    private Image crosshairImage;
    [SerializeField] Color crosshairColorGrappleable;
    [SerializeField] Color crosshairColorNotGrappleable;
    private TMP_Text notGrappleableDistanceText;

    protected override void Start() {
        crosshairImage=transform.parent.parent.parent.Find("Display").Find("Crosshair").GetComponent<Image>();
        notGrappleableDistanceText=transform.parent.parent.parent.Find("Display").Find("Not Grappleable Distance").GetComponent<TMP_Text>();
        base.Start();
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;
    }

    public override void Fire() {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, whatIsGrappleable)) {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0f;
            joint.minDistance = 0f;

            //Adjust these values to fit your game.
            joint.spring = spring;
            joint.damper = damper;
            joint.massScale = massScale;
            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
            Vector3 launch = CalculateJumpVelocity(currentGrapplePosition,grapplePoint);
            // player.GetComponent<Rigidbody>().velocity+=launch;  // Commented out to keep grappling "realistic"
            base.Fire();
        }
    }

    protected override void Update()
    {
        base.Update();
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, whatIsGrappleable)) {
            crosshairImage.color = crosshairColorGrappleable;
            notGrappleableDistanceText.text = "";
        }
        else
        {
            crosshairImage.color = crosshairColorNotGrappleable;
            if (Physics.Raycast(cam.position, cam.forward, out hit, Mathf.Infinity, whatIsGrappleable)) {
                notGrappleableDistanceText.text = Mathf.Ceil(hit.distance - maxDistance).ToString();
            }
            else
            {
                notGrappleableDistanceText.text = "";
            }
        }
    }


    public override void Release() {
        lr.positionCount = 0;
        Destroy(joint);
        base.Release();
    }

    public override void Hold()
    {
        DrawRope();
    }

    private Vector3 currentGrapplePosition;
    
    void DrawRope() {
        //If not grappling, don't draw rope
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