using Ebro.Unity.Behaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirdPersonRBActionController : MovementBehaviour
{
    [Serializable]
    public class MoveSettings
    {
        public float forwardVel = 12;
        public float rotateVel = 100;
        public float jumpVel = 25;
        public float distToGrounded = 0.1f;
        public LayerMask ground;
    }

    [Serializable]
    public class PhysSettings
    {
        public float downAccel = 0.75f;
    }

    [Serializable]
    public class InputSettings
    {
        public float inputDelay = 0.1f;
        public string ForwardAxis = "Vertical";
        public string TurnAxis = "Horizontal";
        public string JumpAxis = "Jump";
    }

    public MoveSettings moveSetting = new MoveSettings();
    public PhysSettings physSetting = new PhysSettings();
    public InputSettings inputSetting = new InputSettings();

    Vector3 velocity = Vector3.zero;
    public Quaternion TargetRotation;
    private Rigidbody rbody;
    float forwardInput, turnInput, jumpInput;

    private void Start()
    {
        this.TargetRotation = transform.rotation;
        rbody = this.GetComponent<Rigidbody>();
    }

    private void GetInput()
    {
        forwardInput = Input.GetAxis(inputSetting.ForwardAxis);
        turnInput = Input.GetAxis(inputSetting.TurnAxis);
        jumpInput = Input.GetAxisRaw(inputSetting.JumpAxis);
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
        rbody.velocity = transform.TransformDirection(velocity);
    }

    private void Run()
    {
        if (Mathf.Abs(forwardInput) > inputSetting.inputDelay)
        {
            velocity.z = moveSetting.forwardVel * forwardInput;
        }
        else
        {
            velocity.z = 0;
        }

        if (Mathf.Abs(turnInput) > inputSetting.inputDelay)
        {
            velocity.x = moveSetting.forwardVel * turnInput;
        }
        else
        {
            velocity.x = 0;
        }
    }

    private void Turn()
    {
        
    }

    void Jump()
    {
        if (jumpInput > 0 && Grounded())
        {
            Debug.Log("jump");
            velocity.y = moveSetting.jumpVel;
        }
        else if (jumpInput == 0 && Grounded())
        {
            Debug.Log("not jump");
            velocity.y = 0;
        }
        else
        {
            Debug.Log("falling down");
            velocity.y -= physSetting.downAccel;
        }
    }

    bool Grounded()
    {
        RaycastHit info;
        var cast = Physics.Raycast(transform.position, Vector3.down, out info);
        var isGround = Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGrounded, moveSetting.ground);
        if (isGround)
        {
            Debug.Log("on ground");
        }
        else
        {
            Debug.Log("not on ground");
        }
        return isGround;
    }
}
