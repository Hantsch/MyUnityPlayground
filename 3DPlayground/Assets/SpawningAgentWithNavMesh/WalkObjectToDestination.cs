using UnityEngine;
using UnityEngine.AI;

public class WalkObjectToDestination : MonoBehaviour
{
    public Transform Goal;

    private void Start()
    {
        var navAgent = this.GetComponent<NavMeshAgent>();
        navAgent.SetDestination(this.Goal.position);
    }
}
