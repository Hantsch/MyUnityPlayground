using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// made from example: https://youtu.be/miMCu5796KM
/// Requires a CharacterController on the Player
/// </summary>
public class JumpAndGravityPlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 15f;
    public float JumpForce = 10f;
    public float Gravity = 14f;
    
    private float VerticalVelocity;
    private CharacterController Controller;

    private void Start()
    {
        this.Controller = this.GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (this.Controller.isGrounded)
        {
            this.VerticalVelocity = -this.Gravity * Time.deltaTime;
            if (InputManager.GetJump(ButtonState.OnPress))
            {
                this.VerticalVelocity = this.JumpForce;
            }
        }
        else
        {
            this.VerticalVelocity -= this.Gravity * Time.deltaTime;
        }

        var moveVector = Vector3.zero;
        moveVector.x = InputManager.GetHorizontal() * this.MovementSpeed;
        moveVector.y = this.VerticalVelocity;
        moveVector.z = InputManager.GetVertical() * this.MovementSpeed;

        this.Controller.Move(moveVector * Time.deltaTime);
    }
}
