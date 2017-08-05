using UnityEngine;

public class ChargeJumpPlayerController : MonoBehaviour
{
    public float ChargeSpeed = 10f;
    public float ForwardMomentum = 4f;

    private bool OnGround;
    private float JumpPressure = 0f;
    private float MinJumpPressure = 2f;
    private float MaxJumpPressure = 10f;
    private Rigidbody Body;
    private Animator Animator;

    private void Start()
    {
        this.OnGround = true;
        this.Body = this.GetComponent<Rigidbody>();
        this.Animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (this.OnGround)
        {
            if (InputManager.GetJump(ButtonState.Pressed))
            {
                if (this.JumpPressure < this.MaxJumpPressure)
                {
                    this.JumpPressure += Time.deltaTime * this.ChargeSpeed;
                }

                this.Animator.SetFloat("jumpPressure", this.JumpPressure);
                this.Animator.speed = 1f + (this.JumpPressure / this.ChargeSpeed);
            }
            else
            {
                if (this.JumpPressure > 0f)
                {
                    this.JumpPressure = Mathf.Clamp(this.JumpPressure, this.MinJumpPressure, this.MaxJumpPressure);
                    this.Body.velocity = new Vector3(this.ForwardMomentum, this.JumpPressure, 0f);
                    this.JumpPressure = 0;
                    this.OnGround = false;
                    this.Animator.SetFloat("jumpPressure", this.JumpPressure);
                    this.Animator.SetBool("onGround", false);
                    this.Animator.speed = 1f;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("ground"))
        {
            this.OnGround = true;
            this.Animator.SetBool("onGround", true);
        }
    }
}
