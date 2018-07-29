using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class bullet : MonoBehaviour {
    public ParticleSystem explosion;
    public GameObject myBullet;
    public GameObject player;
    public Animator npcAnimator;
    public GameObject npcCollider;

    // Use this for initialization
    void Start () {

        myBullet = GetComponent<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	

   void OnDestroy()
    {   
        ParticleSystem fire = Instantiate(explosion, transform.position, transform.rotation);
        //Debug.Log("destroyed");
        Destroy(fire, 0.5f);
        if (npcAnimator != null)
        {
            //Debug.Log("called");
            npcAnimator.SetBool("beingHit", false);
            
        }


        if (npcCollider != null)
        {
            npcCollider.SetActive(false);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this);
        if (other.name == "Body")
        {
            player.GetComponent<PlayerStat>().currentHealth -= 5;
            player.GetComponent<PlayerStat>().isDamaged = true;
            // see whether player health reaches 0

            if (player.GetComponent<PlayerStat>().currentHealth <= 0)
            {
                player.GetComponent<PlayerStat>().currentHealth = 0;
            }

            //player.GetComponentInChildren<Animator>().SetTrigger("Hit");
            
            
            player.GetComponent<PlayerStat>().calculate(player.GetComponent<PlayerStat>().healthBar, player.GetComponent<PlayerStat>().currentHealth);
        }
        else if (other.tag == "NPC")
        {

            npcAnimator = other.GetComponentInChildren<Animator>();
            npcCollider = other.transform.Find("Text").gameObject;
            npcCollider.transform.position = other.transform.position + other.transform.up;
            other.GetComponentInChildren<Animator>().SetBool("isWalking", false);
            other.GetComponentInChildren<Animator>().SetBool("beingHit", true);
            npcCollider.SetActive(true);
            //Destroy(this);
        }
    }



}
