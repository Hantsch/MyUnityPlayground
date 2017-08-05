using System;
using Ebro.Unity.Settings;
using UnityEngine;

public class MyThirdPersonController : MonoBehaviour
{
    public bool Debug;

    public MouseSettings MouseSettings = new MouseSettings();
    public CameraSettings CameraSettings = new CameraSettings();
    public MovementSettings MovementSettings = new MovementSettings();
    public PhysicsSettings PhysicsSettings = new PhysicsSettings();

    private float VerticalVelocity;
    private CharacterController Controller;

    private Vector3 MoveVector;
    private Vector3 LastMove;

    private float CurrentX = 0.0f;
    private float CurrentY = 0.0f;


    private void Start()
    {
        this.Controller = this.GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update()
    {
        this.SnapViewToMouse();
        this.MovePlayer();
    }

    private void MovePlayer()
    {
        if (this.Controller.isGrounded)
        {
            this.MovementSettings.UsedJumps = 0;
            this.MoveVector =
                this.transform.forward * InputManager.GetVertical() +
                this.transform.right * InputManager.GetHorizontal();
            this.MoveVector = this.MoveVector.normalized * this.MovementSettings.MovementSpeed;

            if (InputManager.GetJump(ButtonState.OnPress))
            {
                this.VerticalVelocity = this.MovementSettings.JumpForce;
                this.MovementSettings.UsedJumps++;
            }
        }
        else if (this.MovementSettings.UsedJumps < this.MovementSettings.Jumps && InputManager.GetJump(ButtonState.OnPress))
        {
            this.VerticalVelocity = this.MovementSettings.JumpForce;
            this.MovementSettings.UsedJumps++;
        }

        if (!this.Controller.isGrounded)
        {
            var deltaMove = 
                this.transform.forward * InputManager.GetVertical() +
                this.transform.right * InputManager.GetHorizontal();
            deltaMove = deltaMove.normalized * this.MovementSettings.InAirMovementSpeed * Time.deltaTime;

            this.MoveVector += deltaMove;

            this.VerticalVelocity += this.PhysicsSettings.GetGravityVelocity();
        }

        this.Dash();
        this.DashUp();

        this.MoveVector.y = this.VerticalVelocity;
        this.Controller.Move(this.MoveVector * Time.deltaTime);
    }

    private bool CanDashUp;
    private void DashUp()
    {
        if (this.Controller.isGrounded)
        {
            this.CanDashUp = true;
        }

        if (CanDashUp && Input.GetKeyDown(KeyCode.T))
        {
            this.CanDashUp = false;
            this.VerticalVelocity = 100;
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            this.VerticalVelocity = 4;
            var dashMultiplier = 50;
            if (this.Controller.isGrounded)
            {
                dashMultiplier *= 4;
            }

            this.MoveVector += this.transform.forward * dashMultiplier;
        }
    }

    private void OnGUI()
    {
        if (this.Debug)
        {
            var localVelocity = transform.InverseTransformDirection(this.Controller.velocity);
            GUI.Label(new Rect(0, 0, 400, 100), "z: " + localVelocity.z);
            GUI.Label(new Rect(0, 15, 400, 100), "x: " + localVelocity.x);

            GUI.Label(new Rect(0, 30, 400, 100), "jumps: " + this.MovementSettings.UsedJumps);
        }
    }

    private void SnapViewToMouse()
    {
        this.CurrentX += InputManager.GetHorizontalLook() * this.MouseSettings.SensivityX;
        this.CurrentY += InputManager.GetVerticalLook(this.MouseSettings.InvertMouse) * this.MouseSettings.SensivityY;
        this.CurrentY = Mathf.Clamp(this.CurrentY, this.CameraSettings.MinAngle, this.CameraSettings.MaxAngle);

        this.CameraSettings.Distance += InputManager.GetZoom() * this.CameraSettings.ZoomSpeed;
        this.CameraSettings.Distance = Mathf.Clamp(this.CameraSettings.Distance, this.CameraSettings.MinDistance, this.CameraSettings.MaxDistance);

        this.transform.rotation = Quaternion.Euler(0f, this.CurrentX, 0f);
    }

    private void LateUpdate()
    {
        var offset = new Vector3(0f, 0f, -this.CameraSettings.Distance);

        var rotation = Quaternion.Euler(this.CurrentY, this.CurrentX, 0);
        this.CameraSettings.Camera.transform.position = this.transform.position + (rotation * offset);

        this.CameraSettings.Camera.transform.LookAt(this.transform.position);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!this.Controller.isGrounded && hit.normal.y < 0.1f)
        {
            if (InputManager.GetJump(ButtonState.OnPress))
            {
                this.VerticalVelocity = this.MovementSettings.JumpForce;
                this.MoveVector = hit.normal * this.MovementSettings.MovementSpeed;
            }
        }
    }
}
