using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {
    public Camera camera;
    public bool enabledInput = true;
    public NavMeshAgent agent;
    public float movingSpeed = 50;
    //public Interactable focus;
    public Transform target;
    //public GameObject playerMesh;
    public Animator playerAnimator;


    #region bullets
    public GameObject myBullet;
    public GameObject rifleBullet;
    public GameObject rifle;
    public GameObject gun;
    public GameObject shoulder;
    #endregion

    int layerMask = ~(0 << 5);
    public Inventory inventory;

    public bool fire;


    // Use this for initialization
    void Start () {
        // need data for UI later ...
        camera = FindObjectOfType<Camera>(); // find main camera
        agent = GetComponent<NavMeshAgent>(); // find nav mesh agent
        playerAnimator = GetComponentInChildren<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
        // move player if they press right button
        
        if (Input.GetMouseButtonDown(0) && enabledInput)
        {
            playerAnimator.SetBool("Fire", false);
            //playerAnimator.SetBool("isWalking", true);
            Ray ray = camera.ScreenPointToRay(Input.mousePosition); //casting the camera's screen point to ray
            RaycastHit hit; // shooting out the raycast, used to store position to move

            // see if ray hit somehting or not
           if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) ){
                // move agent
                var direction = hit.point - agent.transform.position;
               
                agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation,
                                                  Quaternion.LookRotation(direction),
                                                 5.0f * Time.deltaTime);
               
                agent.SetDestination(hit.point); // got to where the mouse is on screen
                
            }


            // stop focus any object

        }

        playerAnimator.SetBool("isWalking", !(Vector3.Distance(agent.velocity, Vector3.zero) < 1.5f));





        // check if we hit an interactable object
        
        if(Input.GetMouseButtonDown(1) && enabledInput)
        {
            playerAnimator.SetBool("isWalking", false);
            agent.ResetPath();
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // see if ray hit somehting or not
            if (Physics.Raycast(ray, out hit))
            {
                agent.transform.LookAt(hit.point);
                //gun.transform.LookAt(hit.point);
                playerAnimator.SetBool("Fire", true);
                GameObject newBullet = Instantiate(rifleBullet, myBullet.transform.position, myBullet.transform.rotation);
                //gun.transform.LookAt(hit.point);
                newBullet.transform.LookAt(hit.point);
                newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 500.0f);
                StartCoroutine(stopFire(hit.point));
                Destroy(newBullet, 1.5f);





                /*
                GameObject hitbox = rifle; // generic hit point
                // see if there is a hit box
                if (hit.collider.tag == "Enemy")
                {
                    hitbox = hit.collider.GetComponent<enemyList>().fatal;
                    Debug.Log("yes");
                    //shoulder.transform.LookAt(hitbox.transform);
                    gun.transform.LookAt(hitbox.transform);
                    newBullet.transform.LookAt(hitbox.transform);

                }
                */


                //Debug.Log(hit.collider.gameObject.tag);



            }


            Debug.Log(playerAnimator.GetBool("Fire"));

        }




        //playerAnimator.SetBool("Fire", false);

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

   IEnumerator stopFire(Vector3 hit)
    {
        //float timestamp = 0;
        //Debug.Log("start...");
        
        //StartCoroutine(fireBullet(hit));

        yield return new WaitForSeconds(0.5f);
       
        playerAnimator.SetBool("Fire", false);
        //Debug.Log("stop...");

    }

    IEnumerator fireBullet(Vector3 hit)
    {
        Debug.Log("fire bullet");
        yield return null;
    }

  
    
}
