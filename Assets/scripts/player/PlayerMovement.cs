using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {
    public Camera camera;
    public NavMeshAgent agent;
    public float movingSpeed = 50;
    //public Interactable focus;
    public Transform target;
    
    int layerMask = ~(0 << 5);
    public Inventory inventory;

    // Use this for initialization
    void Start () {
        // need data for UI later ...
        camera = FindObjectOfType<Camera>(); // find main camera
        agent = GetComponent<NavMeshAgent>(); // find nav mesh agent

	}
	
	// Update is called once per frame
	void Update () {
        // move player if they press right button
		if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition); //casting the camera's screen point to ray
            RaycastHit hit; // shooting out the raycast, used to store position to move

            // see if ray hit somehting or not
           if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) ){
                // move agent
                agent.SetDestination(hit.point); // got to where the mouse is on screen
            }

            // stop focus any object
            
        }
        // check if we hit an interactable object
        /*
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // see if ray hit somehting or not
            if (Physics.Raycast(ray, out hit, 100))
            {
                // check if hits an interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>(); 
                if (interactable != null)
                {
                    setFocus(interactable);
                    //Debug.Log("focused");
                }
            }

        }
        */
 
	}

    

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            inventory.addItem(other.GetComponent<Item>());
            other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
            //Debug.Log("hello");
        } 
    }

  
    
}
