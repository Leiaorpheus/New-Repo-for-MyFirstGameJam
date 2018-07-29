using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class WinningCondition : MonoBehaviour {
    public Text count;
    public GameObject notice;
    [SerializeField]
    private int enemyCount;

    void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length / 3;
        count.text = enemyCount.ToString();
    }


    private void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length / 3;
        if (enemyCount == 0)
        {
            count.text = "find the well now...";
        } else
        {
            count.text = enemyCount.ToString();
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Body" && enemyCount == 0)
        {
            SceneManager.LoadScene("win", LoadSceneMode.Single);
        }
        else if (other.name == "Body")
        {
            notice.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Body")
        {
            notice.SetActive(false);
        }
    }
}
