using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{

    CameraController cameraController;


    [SerializeField] float moveSpeed = 5f; 
    [SerializeField] float rotateSpeed = 500f;
    Quaternion targetLocation;
    //called when the game starts 
    private void Awake(){
        //access camera on game awake 
        cameraController = Camera.main.GetComponent<CameraController>();
    }


    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v =Input.GetAxis("Vertical");

        float moveAmount = Mathf.Abs(h) + Mathf.Abs(v); 

        var moveInput = new Vector3(h, 0,v).normalized;

        //make the character move in the direction of the camera
        //PlanarRotation is being accessed from CameraController
        var moveDirection = cameraController.PlanarRotation * moveInput;

        //Create an if statement to only move player if there is input 
        if(moveAmount > 0)
        {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        //make the character face the direction they are moving
        targetLocation = Quaternion.LookRotation(moveDirection);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetLocation, rotateSpeed * Time.deltaTime);

        

        

    }
}
