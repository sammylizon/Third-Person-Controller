using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f; 
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v =Input.GetAxis("Vertical");

        var moveInput = new Vector3(h, 0,v).normalized;

        transform.position += moveInput * moveSpeed * Time.deltaTime;

    }
}
