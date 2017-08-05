using UnityEngine;

public class MouseViewOrbitCamera : MonoBehaviour
{
    public Transform LookAt;
    public Transform RotatePlayer;

    public float SensivityX = 4.0f;
    public float SensivityY = 1.0f;

    public float MinAngle = 0f;
    public float MaxAngle = 85f;

    public float Distance = 10.0f;
    public float MinDistance = 1f;
    public float MaxDistance = 15f;
    public float ZoomSpeed = 0.5f;

    private float CurrentX = 0.0f;
    private float CurrentY = 0.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        this.CurrentX += InputManager.GetHorizontalLook() * this.SensivityX;
        this.CurrentY += InputManager.GetVerticalLook() * this.SensivityY;
        this.CurrentY = Mathf.Clamp(this.CurrentY, this.MinAngle, this.MaxAngle);

        this.Distance += InputManager.GetZoom() * this.ZoomSpeed;
        this.Distance = Mathf.Clamp(this.Distance, this.MinDistance, this.MaxDistance);

        if (this.RotatePlayer != null)
        {
            this.RotatePlayer.rotation = Quaternion.Euler(0f, this.CurrentX, 0f);
        }
    }

    private void LateUpdate()
    {
        var offset = new Vector3(0f, 0f, -this.Distance);

        var rotation = Quaternion.Euler(this.CurrentY, this.CurrentX, 0);
        this.transform.position = this.LookAt.position + (rotation * offset);

        this.transform.LookAt(this.LookAt.position);
    }
}
