using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcMovement : MonoBehaviour
{
    public EditorPathScript pathToFollow;

    public int currentWayPointID = 0;
    public float speed;
    private float reachDistance = 1.0f;
    public float rotationSpeed = 5.0f;
    public string pathName;
    public float lookRadius = 3; // sight
    public NavMeshAgent agent;
    public GameObject DialogueBox;
    public Animator npcAnimator;

    Vector3 lastPosition;
    Vector3 currentPosition;

    // Use this for initialization
    void Start()
    {
        lastPosition = transform.position;
        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
        //pathToFollow = GameObject.Find(pathName).GetComponent<EditorPathScript>();

    }

    // Update is called once per frame
    void Update()
    {

        float NPCToPlayer = Vector3.Distance(agent.transform.position, transform.position);

        // move
        if (NPCToPlayer > lookRadius)
        {
            DialogueBox.SetActive(false);
            moveInWayPoint();
        }
        else
        {
            if (tag == "NPC")
            {
                npcAnimator.SetBool("isWalking", false);
                talkToPlayer();
            }
            else if (tag == "Enemy")
            {
                Debug.Log("attack!");
            }


        }

        // attack




    }

    void moveInWayPoint()
    {
        npcAnimator.SetBool("isWalking", true);
        float distance = Vector3.Distance(pathToFollow.pathObject[currentWayPointID].position, transform.position);

        transform.position = Vector3.MoveTowards(transform.position, pathToFollow.pathObject[currentWayPointID].position, Time.deltaTime * speed);

        var rotation = Quaternion.LookRotation(pathToFollow.pathObject[currentWayPointID].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        if (distance <= reachDistance)
        {
            currentWayPointID++;
        }

        if (currentWayPointID >= pathToFollow.pathObject.Count)
        {
            currentWayPointID = 0;
        }
    }


    void talkToPlayer()
    {
        // talk to player
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            DialogueBox.SetActive(true); // show dialogue box
            DialogueBox.GetComponent<DialogueTrigger>().TriggerDialogue(); // call the trigger dialogue function
            agent.isStopped = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
