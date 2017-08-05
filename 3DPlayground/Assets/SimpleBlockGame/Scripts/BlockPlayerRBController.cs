using UnityEngine;

public class BlockPlayerRBController : MonoBehaviour
{
    public float MovementSpeed = 20f;

    private Rigidbody Body;

    private void Start()
    {
        this.Body = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var inputForce = new Vector3(InputManager.GetHorizontal(), 0f, InputManager.GetVertical()) * this.MovementSpeed * Time.deltaTime;
        this.Body.AddForce(inputForce, ForceMode.VelocityChange);
    }
}
