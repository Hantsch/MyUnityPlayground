using UnityEngine;

public class SimpleFollowTarget : MonoBehaviour
{
    public Transform Target;

    public Vector3 Offset;
    public bool UseOffsetValues;

    private void Start()
    {
        if (!this.UseOffsetValues)
        {
            this.Offset = this.Target.position - this.transform.position;
        }
    }

    private void LateUpdate()
    {
        this.transform.position = this.Target.position - this.Offset;
        this.transform.LookAt(this.Target);
    }
}