using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour {
    public float currentHealth = 100;
    public float MAX = 100;
    public float currentRadiation = 10;
    public float currentWater = 100;
    public float currentAP = 0;
    public Slider healthBar;
    public Image waterBar;
    public Image radBar;
    public Animator playerAnimator;
    public GameObject player;
    public bool isDamaged;
    public bool isDead;

    [SerializeField]
    private const float DEHYDRATION_TIME = 2.0f;
    [SerializeField]
    private float timer = 0;


    void Start()
    {
        calculate(healthBar, currentHealth);
        calculate(waterBar, currentWater);
        calculate(radBar, currentRadiation);
    }

     void Update()
    {
        if (!isDead)
        {

            if (timer >= DEHYDRATION_TIME)
            {
                currentWater -= 2;
                calculate(waterBar, currentWater);
                timer = 0;

                if (currentWater <= 0)
                {
                    currentWater = 0;
                    currentHealth -= 2;
                    calculate(healthBar, currentHealth);
                }

            }

            timer += Time.deltaTime;




            if (isDamaged)
            {

                player.GetComponentInChildren<Animator>().SetBool("Hit", true);
                // player.GetComponent<NavMeshAgent>().ResetPath();         

            }
            else
            {
                player.GetComponentInChildren<Animator>().SetBool("Hit", false);
            }

            isDamaged = false;


            if (currentHealth <= 0)
            {
                isDead = true;
                player.GetComponent<PlayerMovement>().enabledInput = false;
                player.GetComponentInChildren<Collider>().enabled = false;
            }



        } else
        {
            player.GetComponentInChildren<Animator>().SetBool("Hit", false);
            playerAnimator.SetTrigger("Die");         
            player.GetComponent<PlayerMovement>().enabledInput = false;
            player.GetComponentInChildren<Collider>().enabled = false;
            StartCoroutine(restart());
        

        }
        
     }

    // for slider bar
    public void calculate(Slider bar, float stat)
    {
        bar.value = stat / MAX;
    }

    // for image bar
    public void calculate(Image bar, float stat)
    {
        bar.fillAmount = stat / MAX;
    }


    IEnumerator restart()
    {
        yield return new WaitForSeconds(2.0f);
        playerAnimator.enabled = false;
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("restart", LoadSceneMode.Single);
    }



}


