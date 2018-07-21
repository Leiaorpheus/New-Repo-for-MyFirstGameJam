using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class DialogueManger : MonoBehaviour {
    private Queue<string> sentences; // first in first out 
    public Text nameText;
    public Text dialogue;
    public Animator animator;
    public NavMeshAgent player;
  
	
	
    public void startDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        sentences = new Queue<string>();
        //Debug.Log("Starting conversation with " + dialogue.npcName);
        nameText.text = dialogue.npcName;
        sentences.Clear();

        foreach (string s in dialogue.npcLines)
        {
            sentences.Enqueue(s); // add the sentence to the queue

        }

        displaynextSentence();
    }

    public void displaynextSentence()
    {
        // see whether there is further iteems in the queue

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;

        }

        string line = sentences.Dequeue(); // remove first added sentence
        StopAllCoroutines();
        StartCoroutine(TypeSentence(line));


    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogue.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogue.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        player.isStopped = false; // can walk now
        player.ResetPath();
        //Debug.Log("End");
        // player can move

    }
}
