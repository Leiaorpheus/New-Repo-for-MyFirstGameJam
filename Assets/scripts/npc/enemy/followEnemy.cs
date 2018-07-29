using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followEnemy : MonoBehaviour {
    public Transform enemy;
	
	
	// Update is called once per frame
	void Update () {
        if (enemy == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = enemy.position + enemy.up * 7f;
        }
    }
}
