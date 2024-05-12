using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float cameraZoom;
    [SerializeField] float minVertAngle = 5;
    [SerializeField] float maxVertAngle = 5;

    [SerializeField] Vector2 framingOffset;

    [SerializeField] bool invertY;
    [SerializeField] bool invertX;




    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    float rotationY;
    float rotationX;


    public float verticalSensitivity;
    public float horizontalSensitivity;

    float invertXVal;
    float invertYVal;
    // Update is called once per frame
    void Update()
    {
        //invert camera by multiplying by -1

        invertXVal = (invertX) ? -1 : 1;
        invertYVal = (invertY) ? -1 : 1;

        //Use player input for rotation - refactor controller later
        //Horizontal Input
        rotationY += Input.GetAxis("Mouse X") * invertXVal* horizontalSensitivity;

        //limit camera roatation with clamp
        rotationX = Mathf.Clamp(rotationX, minVertAngle, maxVertAngle);

        //Vertical Input
        rotationX += Input.GetAxis("Mouse Y") * invertYVal* verticalSensitivity;

        //Create a 45 degree rotation to use on camera 
        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

        var focusPosition = followTarget.position + new Vector3(framingOffset.x, framingOffset.y);

        //follow player plus add rotation
        transform.position = focusPosition - targetRotation *  new Vector3(0, 0, cameraZoom);
        transform.rotation = targetRotation;
    }

    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY,0);
}
