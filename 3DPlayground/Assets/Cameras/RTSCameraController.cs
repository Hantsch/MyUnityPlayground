using UnityEngine;

// better user: https://www.assetstore.unity3d.com/en/#!/content/43321
public class RTSCameraController : MonoBehaviour
{
    public float PanSpeed = 20f;
    public float PanBorderThickness = 10f;
    public Vector2 PanLimit = new Vector2(40, 50);
    public float ScrollSpeed = 20f;
    public float MinY = 20f;
    public float MaxY = 100f;

    private void Update()
    {
        var newPosition = this.transform.position;

        if (Input.GetKey(KeyCode.E) || Input.mousePosition.y >= Screen.height - this.PanBorderThickness)
        {
            newPosition.z += this.PanSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) || Input.mousePosition.y <= this.PanBorderThickness)
        {
            newPosition.z -= this.PanSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.F) || Input.mousePosition.x >= Screen.width - this.PanBorderThickness)
        {
            newPosition.x += this.PanSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S) || Input.mousePosition.x <= this.PanBorderThickness)
        {
            newPosition.x -= this.PanSpeed * Time.deltaTime;
        }

        var scroll = Input.GetAxis("Mouse ScrollWheel");
        newPosition.y -= scroll * this.ScrollSpeed * 100f * Time.deltaTime;

        //newPosition.x = Mathf.Clamp(newPosition.x, -PanLimit.x, PanLimit.x);
        newPosition.y = Mathf.Clamp(newPosition.y, this.MinY, this.MaxY);
        //newPosition.z = Mathf.Clamp(newPosition.x, -PanLimit.y, PanLimit.y);

        this.transform.position = newPosition;
    }
}
