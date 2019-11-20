using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{

    public float graivityModifier = 1f;

    protected Rigidbody rb;
    protected Vector3 velocity;

    protected const float minMoveDistance = 0.001f;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        velocity += graivityModifier * Physics.gravity * Time.deltaTime;

        Vector3 deltaPosition = velocity * Time.deltaTime;

        Vector3 move = Vector3.up*deltaPosition.y;

        Movement(move);
        
    }

    void Movement(Vector3 move)
    {
        float distance = move.magnitude;
        
        if (distance > minMoveDistance)
        {
           
        }

        rb.position = rb.position + move;
    }
}
