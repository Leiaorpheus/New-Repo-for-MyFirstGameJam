using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rifleFire : bullet {
    public float damage;
    protected virtual void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy")
        {
            Destroy(this);
            other.GetComponent<enemyList>().health -= damage;
            other.GetComponent<enemyList>().healthBar.fillAmount = other.GetComponent<enemyList>().health / other.GetComponent<enemyList>().maxHealth;
            other.GetComponent<enemyList>().isDamaged = true;
            //Debug.Log("hitted" + other.GetComponent<enemyList>().health);
            if (other.GetComponent<enemyList>().health <= 0)
            {
                other.GetComponent<enemyList>().health = 0; // set health to 0 if less than 0
                other.GetComponent<enemyList>().dead = true;
            } 

            //Debug.Log("hmmm");
        }
    }
}
