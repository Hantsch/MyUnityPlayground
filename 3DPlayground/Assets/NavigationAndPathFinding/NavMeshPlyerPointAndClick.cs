using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPlyerPointAndClick : MonoBehaviour
{
    NavMeshAgent NavAgent;

    private void Start()
    {
        this.NavAgent = this.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                this.NavAgent.SetDestination(hit.point);
            }
        }
    }
}
