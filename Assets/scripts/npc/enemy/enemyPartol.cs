using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPartol : enemyBase{
    //GameObject enemy;
    public GameObject[] waypoints;
    int currentWP;
    //public float distance = 1.0f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //enemy = animator.gameObject;
        //player = enemy.GetComponent<enemyList>().mainEnemy; // finding player object
        base.OnStateEnter(animator,  stateInfo, layerIndex); // call parent class
        waypoints = new GameObject[enemy.GetComponent<enemyList>().WPS.GetComponent<EditorPathScript>().pathObject.Count];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = enemy.GetComponent<enemyList>().WPS.transform.GetChild(i).gameObject;
        }

        currentWP = 0;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (waypoints.Length == 0) return; // do nothing if there is no way point

        if (currentWP < waypoints.Length)
        {   
            Vector3 newPos = Vector3.MoveTowards(enemy.transform.position, waypoints[currentWP].transform.position, walkSpeed* Time.deltaTime);
           
            enemy.transform.position = newPos;
            var direction = waypoints[currentWP].transform.position - enemy.transform.position;
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation,
                                                  Quaternion.LookRotation(direction),
                                                  rotSpeed * Time.deltaTime);

            if (newPos == waypoints[currentWP].transform.position)
            {
                currentWP++;

                if (currentWP >= waypoints.Length)
                {
                    currentWP = 0; // set it back to 0 if it is out of range
                }
            }
        }
        
        // if approaches waypoint enough
        /*
        if (Vector3.Distance(waypoints[currentWP].transform.position, enemy.transform.position) < distance)
        {
            currentWP++;

            if (currentWP >= waypoints.Length)
            {
                currentWP = 0; // set it back to 0 if it is out of range
            }

            Debug.Log("waypoint: " + waypoints[currentWP].name);
        } 

        // rotate towards target
        var direction =  waypoints[currentWP].transform.position - enemy.transform.position;

        // turn to waypoint
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, 
                                                    Quaternion.LookRotation(direction), 
                                                    1.0f * Time.deltaTime); // gradually rotate

        // translation: go forward in the direction of waypoint
        enemy.transform.Translate(5 * Vector3.forward * Time.deltaTime);

        */




    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}
    
}
