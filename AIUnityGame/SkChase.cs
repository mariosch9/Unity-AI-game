using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkChase : MonoBehaviour {

    public Transform player;
    public Transform head;
    static Animator anim; //can remove static to have more skeletons

    string state = "patrol";
    public GameObject[] waypoints;
    int currentWP = 0;
    public float rotSpeed = 0.2f;
    public float speed = 1.5f;
    float accuracyWP = 5.0f;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = player.position - this.transform.position;
        direction.y = 0; //rotation along y so that he is not tipping when coming close anymore

        float angle = Vector3.Angle(direction,head.up);

        if(state == "patrol" && waypoints.Length > 0)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
            if(Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP)
            {
                currentWP = Random.Range(0, waypoints.Length); //random waypoints
                //currentWP++;
                //if(currentWP >= waypoints.Length)    ---- fixed sequence of waypoints ----
                //{
                    //currentWP = 0;
                //}
            }
            //rotate towards waypoint
            direction = waypoints[currentWP].transform.position - transform.position; //workout the direction
            this.transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime); //rotate towards the waypoint
            this.transform.Translate(0, 0, Time.deltaTime * speed); //pushing it forward
        }
        
        if (Vector3.Distance(player.position, this.transform.position) < 10 && (angle < 30 || state == "pursuing"))
        {

            state = "pursuing";

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);

            
            if(direction.magnitude > 5)
            {
                this.transform.Translate(0, 0, Time.deltaTime * speed); //move him forward by translation on his z axis
                //also we are able to outrun him and he loses interest 
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);
            }
            else
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
            }
        }
		else
        {
            
            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);
            state = "patrol";
        }
	}
}
