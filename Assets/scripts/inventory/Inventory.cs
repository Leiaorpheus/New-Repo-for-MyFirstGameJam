using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    private RectTransform inventoryRect; // rectangle holding inventory image
    private float inventoryWidth, inventoryHeight;
    public int slots, rows;
    public float slotPaddingLeft, slotPaddingTop;
    public float slotSize;
    public GameObject slotPrefab;
    private List<GameObject> allSlots;
    private static int emptySlots;

    [SerializeField]
    private static Slots from,to;

    public GameObject iconPrefab;
    public static GameObject hoverObject;
    public Canvas canvas;
    private float hoverYOffset;
    public EventSystem eventSystem;
    public static GameObject player;

    // adding text
    public GameObject parentTooltip;
    private static GameObject toolTip;
    public  Text sizeTextObject;
    public Text visualTextObject;

    private static Text sizeText;
    private static Text visualText;


    public static int EmptySlots
    {
        get
        {
            return emptySlots;
        }

        set
        {
            emptySlots = value;
        }
    }

    // Use this for initialization
    void Start () {
        inventoryRect = GetComponent<RectTransform>();
        allSlots = new List<GameObject>();
        createLayout();
        toolTip = parentTooltip; // assign to all instances
        sizeText = sizeTextObject;
        visualText = visualTextObject;
        player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () {

        if(Input.GetMouseButtonUp(0))
        {   // delete inventory
            if(!eventSystem.IsPointerOverGameObject(-1) && from != null)
            {
                from.GetComponent<Image>().color = Color.white;
                from.clearSlot();
                Destroy(GameObject.Find("Hover"));
                hoverObject = null;
                from = null;
                to = null;
            }
        }


		if (hoverObject != null)
        {
            // code making icon hover around screen
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out position);
            position.Set(position.x, position.y - hoverYOffset); //??
            hoverObject.transform.position = canvas.transform.TransformPoint(position);
            player.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
        }
	}

    private void createLayout()
    {   // how many empty slots
        EmptySlots = slots;
        hoverYOffset = slotSize * 0.01f;
        // calculate inventory
        inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
        inventoryHeight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;
        // set properties of inventory
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHeight);

        // spawn rows and columns
        int columns = slots / rows;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject newSlot = Instantiate(slotPrefab); // create an isntance
                RectTransform slotRect = newSlot.GetComponent<RectTransform>(); // get the transform
                newSlot.name = "Slot"; // give these instances a name
                //newSlot.GetComponent<Slots>().ItenNum = j;// set item num
                newSlot.transform.SetParent(this.transform.parent);
                slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (j + 1) + slotSize * j, -(slotPaddingTop * (i + 1) + slotSize * i), 0);

                // set size of the widget
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

                // add it to all slot
                allSlots.Add(newSlot);
            }
        }
    }

    public bool addItem(Item item)
    {
        if (item.maxSize == 1)
        {
            placeEmpty(item); // unstackable
            return true;
        }
        else
        {
            // stackable
            foreach(GameObject slot in allSlots )
            {
                Slots temp = slot.GetComponent<Slots>();
                if (!temp.isEmpty)
                {
                    // see if it is stackable
                    if (temp.currentItem.type == item.type && temp.isAvailable)
                    {
                        // stack now
                        temp.addItem(item);
                        return true;
                    }

                }
            }
            // one is fully stacked
            if (EmptySlots > 0)
            {
                placeEmpty(item); // stack again
                return true;
            }

        }

        return false;

    }

    private bool placeEmpty(Item item)
    {
        if (EmptySlots > 0)
        {
            foreach(GameObject slot in allSlots)
            {
                Slots temp = slot.GetComponent<Slots>();
                if (temp.isEmpty)
                {
                    temp.addItem(item); // add to empty slot
                    EmptySlots--; // one less slot
                    return true;
                }
            }
        }
        return false;
    }

    // move item
    public void MoveItem(GameObject clicked)
    {
        if (from == null)
        {
            if (!clicked.GetComponent<Slots>().isEmpty)
            {
                from = clicked.GetComponent<Slots>(); // initialize from
                from.GetComponent<Image>().color = Color.gray;


                hoverObject = Instantiate(iconPrefab); // an instance of hovering object
                hoverObject.GetComponent<Image>().sprite = clicked.GetComponent<Image>().sprite; //get the image
                hoverObject.name = "Hover"; // change hover icon's name

                RectTransform hoverTransform = hoverObject.GetComponent<RectTransform>();
                RectTransform clickedTransform = clicked.GetComponent<RectTransform>();

                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

                // make it visible
                hoverObject.transform.SetParent(GameObject.Find("InventoryCanvas").transform, true);
                hoverObject.transform.localScale = from.gameObject.transform.localScale; // set local scale
            }
        }
        else if (to == null)
        {
            to = clicked.GetComponent<Slots>();
            Destroy(GameObject.Find("Hover"));

        }

        if (from != null && to != null)
        {
            //swap two items
            Stack<Item> tmpTo = new Stack<Item>(to.Items); // store to as temp var
            to.swapItem(from.Items);
            if (tmpTo.Count == 0)
            {
                from.clearSlot();
            } else
            {
                from.swapItem(tmpTo);
            }


            from.GetComponent<Image>().color = Color.white; // reset color
            // resetting
            from = null;
            to = null;
            hoverObject = null; // done moving
        }

    }

    // showing the tooltip
    public void showTooltip(GameObject slot)
    {
        Slots tooltipSlot = slot.GetComponent<Slots>();

        if (!tooltipSlot.isEmpty && hoverObject == null)
        {
            visualText.text = tooltipSlot.currentItem.getTooltip();
            sizeText.text = visualText.text;

            toolTip.SetActive(true);
            toolTip.GetComponent<RectTransform>().SetAsLastSibling();

            // see whether is left or right column
            float xPos = slot.transform.position.x + slotPaddingLeft + slot.GetComponent<RectTransform>().sizeDelta.x / 2.0f;
            float yPos = slot.transform.position.y - slotPaddingTop - slot.GetComponent<RectTransform>().sizeDelta.y / 2.0f;
            toolTip.transform.position = new Vector2(xPos, yPos);

        }
    }

    // hide tooltip
    public void hideTooltip(GameObject slot)
    {
        toolTip.SetActive(false);
    }
}
