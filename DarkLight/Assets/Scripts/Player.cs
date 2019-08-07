using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 moveVector;
    Vector3 lastMotion;
    CharacterController controller;
    float inputDirection;

    //vertical movement variables
    float verticalVelocity;
    float gravity = 1;
    public float jumpForce = 10;
    public float isGroundedBuffer = 0.1f;
    

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
        

        //horizontal movement
        moveVector = Vector3.zero;
        inputDirection = Input.GetAxis("Horizontal") * speed;

        //vertical movement
        if (IsControllerGrounded())
        {
            verticalVelocity = Input.GetAxis("Jump")*jumpForce;
            moveVector.x = inputDirection;

        }
        else
        {
            verticalVelocity -= gravity;
            moveVector.x = lastMotion.x;
        }

        moveVector.y = verticalVelocity;
        controller.Move(moveVector * Time.fixedDeltaTime);
        lastMotion = moveVector;
        
    }

    private bool IsControllerGrounded()
    {
        Vector3 leftRayStart;
        Vector3 rightRayStart;

        leftRayStart = controller.bounds.center;
        rightRayStart = controller.bounds.center;

        leftRayStart.x -= controller.bounds.extents.x;
        leftRayStart.x += controller.bounds.extents.x;

        Debug.DrawRay(leftRayStart, Vector3.down, Color.red);
        Debug.DrawRay(rightRayStart, Vector3.down, Color.green);

        if (Physics.Raycast(leftRayStart, Vector3.down, (controller.height) / 2 + isGroundedBuffer))
            return true;

        if (Physics.Raycast(rightRayStart, Vector3.down, (controller.height) / 2 + isGroundedBuffer))
            return true;

        return false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        //wall jump
        if (controller.collisionFlags == CollisionFlags.Sides && !IsControllerGrounded())
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.DrawRay(hit.point, hit.normal, Color.red, 2f);
                moveVector = hit.normal * speed;
                verticalVelocity = jumpForce;
            }
        }
    }


}
