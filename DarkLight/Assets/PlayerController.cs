using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 10;
    public float jumpForce = 10;
    [HideInInspector]
    public CharacterController controller;

    public float gravityScale = 10;

    private Vector3 moveDirection;
  
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        print(controller.isGrounded);
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical")*moveSpeed);

        if (controller.isGrounded)
        {
            moveDirection.y = 0;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            moveDirection.y += (Physics.gravity.y * gravityScale * Time.deltaTime);
        }
        
        controller.Move(moveDirection * Time.deltaTime);
        

    }
}
