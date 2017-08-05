using UnityEngine;

public class ThirdPersonActionMovement : MonoBehaviour
{
    public float MovementSpeed = 20f;
    public float JumpForce = 28f;
    public float GravityScale = 10f;
    public float MovementLockTimout = 3f;

    private float VerticalVelocity;
    private CharacterController Controller;

    private Vector3 MoveDirection;
    private bool LockedMovemet;
    private float MovementLockedTime;

    private void Start()
    {
        this.Controller = this.GetComponent<CharacterController>();
    }

    private void Update()
    {
        this.UnlockMovement();

        if (this.Controller.isGrounded)
        {
            if (InputManager.GetJump(ButtonState.OnPress))
            {
                this.VerticalVelocity = this.JumpForce;
            }
        }
        else
        {
            this.VerticalVelocity += Physics.gravity.y * this.GravityScale * Time.deltaTime;
        }

        if (!this.LockedMovemet)
        {
            this.MoveDirection =
                this.transform.forward * InputManager.GetVertical() +
                this.transform.right * InputManager.GetHorizontal();
            this.MoveDirection = this.MoveDirection.normalized * this.MovementSpeed;
        }
        
        this.MoveDirection.y = this.VerticalVelocity;
        this.Controller.Move(this.MoveDirection * Time.deltaTime);
    }

    private void UnlockMovement()
    {
        if (this.LockedMovemet)
        {
            if (this.Controller.isGrounded || this.MovementLockedTime > this.MovementLockTimout)
            {
                this.LockedMovemet = false;
            }
            this.MovementLockedTime += Time.deltaTime;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!this.Controller.isGrounded && hit.normal.y < 0.1f)
        {
            if (InputManager.GetJump(ButtonState.OnPress))
            {
                this.LockedMovemet = false;
                this.VerticalVelocity = this.JumpForce;
                this.MoveDirection = hit.normal * this.MovementSpeed;
                this.LockMovement();
            }
        }
    }

    private void LockMovement()
    {
        this.LockedMovemet = true;
        this.MovementLockedTime = 0f;
    }
}
