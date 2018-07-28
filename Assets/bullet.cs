using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bullet : MonoBehaviour {
    public ParticleSystem explosion;
    public GameObject myBullet;
    public GameObject player;

	// Use this for initialization
	void Start () {

        myBullet = GetComponent<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   void OnDestroy()
    {
        ParticleSystem fire = Instantiate(explosion, transform.position, transform.rotation);
        //Debug.Log("destroyed");
        Destroy(fire, 0.5f);

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
    }



}
