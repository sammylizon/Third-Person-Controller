using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float cameraZoom;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    float rotationY;
    // Update is called once per frame
    void Update()
    {

        //Use player input for rotation - refactor controller later
        rotationY += Input.GetAxis("Mouse X");

        //Create a 45 degree rotation to use on camera 
        var targetRotation = Quaternion.Euler(0, rotationY, 0);

        //follow player plus add rotation
        transform.position = followTarget.position - targetRotation *  new Vector3(0, 0, cameraZoom);
        transform.rotation = targetRotation;
    }
}
