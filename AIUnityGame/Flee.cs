using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flee : MonoBehaviour {

    private static float lookRadius = 10f;
    public GameObject target;

	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance <= lookRadius)
            runaway();
		
	}

    void runaway()
    {
        float maxSpeed = Time.deltaTime * 25f;
        Vector3 velocity = (transform.position - target.transform.position * maxSpeed);

        //calculate the shortest path between the one seeking and the target
        //set that to the desired velocity
        Vector3 desiredVelocity = (transform.position - target.transform.position).normalized * maxSpeed;
        Vector3 steering = desiredVelocity - velocity;

        float maxForce = 1.2f;
        steering = Vector3.ClampMagnitude(steering, maxForce);

        velocity = Vector3.ClampMagnitude(velocity + steering, maxSpeed);
        transform.position = transform.position + velocity;
    }

    //draw the range 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
