using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{

    [SerializeField]
    private Camera cam;
    
    private Vector3 velocity;
    private Vector3 rotation;
    private Vector3 Vertrotation;
    private Vector3 jumped;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }
    
    public void RotateVert(Vector3 _Vertrotation)
    {
        Vertrotation = _Vertrotation;
    }

    public void Gohigh(Vector3 _jumped)
    {
        jumped = _jumped;
    }
    
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    private void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        cam.transform.Rotate(-Vertrotation);
    }

    private void PerformJump()
    {
        rb.MovePosition(rb.position + jumped);
    }

}

