using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBase : StateMachineBehaviour
{
    public GameObject enemy;
    public GameObject player;
    //public GameObject playerCenter;
    public float walkSpeed = 5.0f;
    public float rotSpeed = 5.0f;
    public float runSpeed = 10.0f;
    



    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        player = enemy.GetComponent<enemyList>().returnEnemy(); // finding player object
       
    }
}
