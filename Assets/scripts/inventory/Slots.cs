using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slots : MonoBehaviour, IPointerClickHandler {
    private Stack<Item> items;
    public Text stackTxt; // text on stack
    public Sprite slotEmpty, slotHighlight;
    public static GameObject player;
    //private int itenNum = 0;
    //public Sprite slotEmpty;
    //public Sprite slotHighlight;
    public bool isEmpty
    {
        get { return Items.Count == 0; }
    }

    // see if stackable
    public Item currentItem
    {
        get { return Items.Peek(); }// return what's currently on top of stack
    }

    public bool isAvailable 
    {
        get { return currentItem.maxSize > Items.Count; } // see if out of limitation
    }

    public Stack<Item> Items
    {
        get
        {
            return items;
        }

        set
        {
            items = value;
        }
    }

    /*
    public int ItenNum
    {
        get
        {
            return itenNum;
        }

        set
        {
            itenNum = value;
        }
    }
    */

    // Use this for initialization
    void Start () {
        Items = new Stack<Item>();
        RectTransform slotRect = GetComponent<RectTransform>();
        RectTransform textRect = stackTxt.GetComponent<RectTransform>();
        player = GameObject.FindGameObjectWithTag("Player");

        // set the sect of text ???
        int txtScaleFactor = (int)(slotRect.sizeDelta.x * 0.8);
        stackTxt.resizeTextMaxSize = txtScaleFactor;
        stackTxt.resizeTextMinSize = txtScaleFactor;

        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
    }
	
	
    // add single item
    public void addItem(Item item)
    {
        Items.Push(item); // add to items
        if (Items.Count > 1)
        {
            stackTxt.text = (Items.Count).ToString(); // stack text update
        }
        changeSprite(item.spriteNeutural, item.spriteHighlighted);
    }

    // swap items
    public void swapItem(Stack<Item> items)
    {
        this.Items = new Stack<Item>(items); // replace
        stackTxt.text = items.Count > 1 ? (items.Count).ToString() : ""; // update text
        changeSprite(currentItem.spriteNeutural, currentItem.spriteHighlighted);
    }


    private void changeSprite(Sprite normal, Sprite highlight)
    {   // ???
        GetComponent<Image>().sprite = normal;
        SpriteState st = new SpriteState();
        st.highlightedSprite = highlight;
        st.pressedSprite = normal;
        GetComponent<Button>().spriteState = st;
    }

    private void useItem()
    {   // if the slot isn't empty
        if (!isEmpty)
        {
            Items.Pop().Use(); // throw away this item
            stackTxt.text = Items.Count > 1 ? (Items.Count).ToString() : ""; // update text
        } 

        if(isEmpty)
        {
            changeSprite(slotEmpty, slotHighlight);
            Inventory.EmptySlots++;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            useItem();
            //Debug.Log("yes");
        }


    }

    public void clearSlot()
    {
        items.Clear();
        changeSprite(slotEmpty, slotHighlight);
        stackTxt.text = string.Empty; // clear text
    }

    public void stopMoving()
    {
        player.GetComponent<NavMeshAgent>().ResetPath();
    }
}
