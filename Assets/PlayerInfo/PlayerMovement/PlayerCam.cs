using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sX;
    public float sY;

    public Transform orientation;
    public Transform cameraPosition;

    float xRotation;
    float yRotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sX;
        float mY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sY;

        yRotation += mX;
        xRotation -= mY;
        xRotation = Mathf.Clamp(xRotation,-90f,90f);

        transform.rotation = Quaternion.Euler(xRotation,yRotation,0);
        cameraPosition.rotation = Quaternion.Euler(xRotation,yRotation,0);
        orientation.rotation = Quaternion.Euler(0,yRotation,0);
    }
}
