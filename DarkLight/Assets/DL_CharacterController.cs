using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DL_CharacterController : MonoBehaviour
{
    public float speed = 12f;
    public float jumpForce = 3f;

    CharacterController myController;

    float gravity = -9.8f;
    public float gravityMultiplier = 2;

    Vector3 velocity;

    //Ground Check
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    void Start()
    {
        myController = GetComponent<CharacterController>();
        gravity = gravity * gravityMultiplier;
        
    }


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump") || Input.GetAxis("Vertical") > 0)
            {
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            }
        }

        float x = Input.GetAxis("Horizontal");
        
        Vector3 move = new Vector3(x, 0, 0);

        myController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        myController.Move(velocity * Time.deltaTime);


    }
}
