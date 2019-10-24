using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [System.Serializable]
    public class MoveSettings
    {
        public float forwardVel = 12;
        public float rotateVel = 100;
        public float jumpVel = 25;
        public float distToGround = 0.1f;
        public LayerMask groundMask;
    }

    [System.Serializable]
    public class PhysSettings
    {
        public float gravity = 0.75f;
    }

    [System.Serializable]
    public class InputSettings
    {
        public float inputDelay = 0.1f;
        public string FORWARD_AXIS = "Vertical";
        public string TURN_AXIX = "Horizontal";
        public string JUMP_AXIX = "Jump";

    }

    public MoveSettings moveSettings = new MoveSettings();
    public PhysSettings physSettings = new PhysSettings();
    public InputSettings inputSettings = new InputSettings();

    Vector3 velocity = Vector3.zero;
    Quaternion targetRotation;
    Rigidbody rb;
    float forwardInput, turnInput, jumpInput;

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, moveSettings.groundMask);
    }

    private void Start()
    {
        targetRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
        forwardInput = turnInput = jumpInput = 0;
    }

    private void Update()
    {
        GetInput();
        Turn();
    }

    private void FixedUpdate()
    {
        Run();
        Jump();

        rb.velocity = transform.TransformDirection(velocity);
    }

    void GetInput()
    {
        forwardInput = Input.GetAxis(inputSettings.FORWARD_AXIS);
        turnInput = Input.GetAxis(inputSettings.TURN_AXIX);
        jumpInput = Input.GetAxisRaw(inputSettings.JUMP_AXIX);

    }

    void Run()
    {
        if (Mathf.Abs(forwardInput) > inputSettings.inputDelay)
        {
            velocity.z = moveSettings.forwardVel * forwardInput;
        }
        else
        {
            velocity.z = 0;
        }
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > inputSettings.inputDelay)
        {
            targetRotation *= Quaternion.AngleAxis(moveSettings.rotateVel * turnInput * Time.deltaTime, Vector3.up);
        }
        transform.rotation = targetRotation;
    }

    void Jump()
    {
        if(jumpInput>0 && Grounded())
        {
            velocity.y = moveSettings.jumpVel;
        }

        else if (jumpInput==0 && Grounded())
        {
            velocity.y = 0;
        }

        else
        {
            velocity.y -= physSettings.gravity;
        }
    }
}
