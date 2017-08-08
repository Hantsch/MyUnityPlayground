using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var goal = other.gameObject.GetComponent<WalkObjectToDestination>();
        if (goal != null)
        {
            Destroy(other.gameObject);
        }
    }
}
