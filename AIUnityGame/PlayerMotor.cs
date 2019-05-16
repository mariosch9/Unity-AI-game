using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour {

    NavMeshAgent agent;

	
	void Start () {
        agent = GetComponent<NavMeshAgent>();
		
	}
	
    //move to the destination point
	public void MovetoPoint (Vector3 point)
    {
        agent.SetDestination(point);
    }
}
