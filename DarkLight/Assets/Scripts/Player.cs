using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 moveVector;
    CharacterController controller;
    float inputDirection;

    //vertical movement variables
    float verticalVelocity;
    float gravity = 1;
    public float jumpForce = 10;
    

    //horizontal movement variables
    float speed;
    public float walkSpeed = 5;
    public float runSpeed = 10;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = walkSpeed;
    }

    void FixedUpdate()
    {
        print(controller.isGrounded);
        //horizontal movement
        inputDirection = Input.GetAxis("Horizontal") * speed;

        //vertical movement
        if (controller.isGrounded)
        {
            verticalVelocity = 0;
                
            if (Input.GetAxis("Jump")>0)
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity;
        }
        
        moveVector = new Vector3(inputDirection, verticalVelocity, 0);
        controller.Move(moveVector * Time.fixedDeltaTime);         
        
    }

     
}
