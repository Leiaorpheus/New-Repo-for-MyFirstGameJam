using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Dialogue {
    public string npcName; // name of NPC

    [TextArea(3, 10)] // how big we can enter text
    public string[] npcLines; // sentences to say
 
    
}
