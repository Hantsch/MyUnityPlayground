using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAndGravityPlayerMovementWJ : MonoBehaviour
{
    public float MovementSpeed = 8f;
    public float JumpForce = 8f;
    public float Gravity = 25f;

    private float VerticalVelocity;
    private CharacterController Controller;

    private Vector3 MoveVector;
    private Vector3 LastMove;

    private void Start()
    {
        this.Controller = this.GetComponent<CharacterController>();
    }

    private void Update()
    {
        this.MoveVector = Vector3.zero;
        this.MoveVector.x = InputManager.GetHorizontal();
        this.MoveVector.z = InputManager.GetVertical();

        if (this.Controller.isGrounded)
        {
            this.VerticalVelocity = -1;

            if (InputManager.GetJump(ButtonState.OnPress))
            {
                this.VerticalVelocity = this.JumpForce;
            }
        }
        else
        {
            this.VerticalVelocity -= this.Gravity * Time.deltaTime;
            this.MoveVector = this.LastMove;
        }

        this.MoveVector.y = 0;
        this.MoveVector.Normalize();
        this.MoveVector *= this.MovementSpeed;
        this.MoveVector.y = this.VerticalVelocity;

        this.Controller.Move(this.MoveVector * Time.deltaTime);
        this.LastMove = this.MoveVector;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!this.Controller.isGrounded && hit.normal.y < 0.1f)
        {
            if (InputManager.GetJump(ButtonState.OnPress))
            {
                this.VerticalVelocity = this.JumpForce;
                this.MoveVector = hit.normal * this.MovementSpeed;
            }
        }
    }
}
