using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{

    CameraController cameraController;
    Animator animator;
    CharacterController controller;
    [SerializeField] float moveSpeed = 5f; 
    [SerializeField] float rotateSpeed = 500f;
    Quaternion targetRocation;

    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] Vector3 groundCheckOffset; 
    [SerializeField] LayerMask groundLayer; 

    bool isGrounded;
    float ySpeed;


    //called when the game starts 
    private void Awake()
    {
        //access camera on game awake 
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v =Input.GetAxis("Vertical");

        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v)); 

        var moveInput = new Vector3(h, 0,v).normalized;


        //make the character move in the direction of the camera
        //PlanarRotation is being accessed from CameraController
        var moveDirection = cameraController.PlanarRotation * moveInput;

        //check if player is grounded 
        GroundCheck();
        //add gravity
        if(isGrounded){
            ySpeed = -0.5f;

        }else{
            //velocity on Y axis to simulate gravity 
            //gravity setting in Unity preferences
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }

        var velocity = moveDirection * moveSpeed;
        velocity.y = ySpeed;

        //move player using character controller
        controller.Move(velocity * Time.deltaTime);

        //Create an if statement to only move player if there is input 
        if(moveAmount > 0)
        {       
            //make the character face the direction they are moving
            targetRocation = Quaternion.LookRotation(moveDirection);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRocation, rotateSpeed * Time.deltaTime);

        animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);

    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
    }

    //Draw gizmo to debug the groundcheck for gravity 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0,1,0,0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset),groundCheckRadius);
    }
}
