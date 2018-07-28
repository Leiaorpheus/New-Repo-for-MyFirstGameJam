using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyList : MonoBehaviour {

    public GameObject mainEnemy;
    public GameObject bullet;
    public Transform location;
    public Material bulletMaterial;
    public Transform rightHand;
    public Transform target;
    public Transform pistol;
    public float timestamp;
    public GameObject fatal;
    public float health = 250;
    public bool isDamaged;
    public bool dead;
    public GameObject self;
    public Image healthBar;
    public float maxHealth = 250;
    public GameObject WPS;

    #region for enemy
    public Animator enemyAnim;
    #endregion

    public GameObject returnEnemy()
    {
        return mainEnemy;
    }

   

    void Start()
    {
        enemyAnim = gameObject.GetComponent<Animator>();
       
    }

    void Update()
    {
        enemyAnim.SetFloat("distance", Vector3.Distance(transform.position, mainEnemy.transform.position));
        //Debug.Log(Vector3.Distance(transform.position, enemyAnim.transform.position));

       if(isDamaged)
        {
            if (dead)
            {
                //AnimatorStateInfo asi = enemyAnim.GetCurrentAnimatorStateInfo(0);

                enemyAnim.SetBool("isDead", true);
                StartCoroutine(stopDeathAnimation());



            } else
            {
                //Debug.Log(".......");
               // enemyAnim.SetBool("hit", true);
                enemyAnim.SetBool("hit", true);
                StartCoroutine(stopHit());
            }

            //isDamaged = false;
            //enemyAnim.SetBool("hit", false);
        }


    }

    public void startFiring()
    {
        
        if (Time.time > timestamp && !mainEnemy.GetComponent<PlayerStat>().isDead)
        {
            
            GameObject bulletObj = Instantiate(bullet, location.position, location.rotation);
            bulletObj.GetComponent<Renderer>().material = bulletMaterial;
            Rigidbody bulletBody = bulletObj.AddComponent<Rigidbody>(); // add rigidbody to bullet
            bulletObj.transform.localScale = new Vector3(3, 3, 3);                                                            //bulletBody.isKinematic = true;
            bulletBody.useGravity = false;
            bulletObj.GetComponent<Rigidbody>().AddForce(bulletObj.transform.forward * 500.0f);
            // destroy bullet after 1 seconds
            Destroy(bulletObj, 1f);
            timestamp = Time.time + 0.3f;
        }

    }

  



    IEnumerator stopDeathAnimation()
    {
        yield return new WaitForSeconds(1.2f);
        enemyAnim.SetBool("isDead", false);
        Destroy(self);
        //self.SetActive(false);
    }


    IEnumerator stopHit()
    {
        isDamaged = false;
        yield return new WaitForSeconds(0.5f);
        enemyAnim.SetBool("hit", false);
    }



}
