using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public enum ItemType {potion, mana, berry};
    public enum Quality {common, uncommon,rare, epic, fantastic};
    public GameObject player;

    public ItemType type;
    public Quality quality;
    public Sprite spriteNeutural;
    public Sprite spriteHighlighted;

    [TextArea(10,2)]
    public string description;
    public string itemName;
    public float health, mana, radation, water;


    public int maxSize = 2; // how many time it can be stacked

    public void Use()
    {
        switch(type)
        {
            case ItemType.mana:
                Debug.Log("You picked up mana");
                break;
            case ItemType.potion:
                Debug.Log("You picked up potion");
                break;
            case ItemType.berry:
                Debug.Log("You picked up berry");
                break;
            default:
                Debug.Log("error occured");
                break;
        }

        addStat();


    }

    public string getTooltip()
    {
        string stat = string.Empty;
        string color = string.Empty;
        string newLine = string.Empty;

        if (description != string.Empty)
        {
            newLine = "\n";
        }

        switch (quality)
        {
            case Quality.common:
                color = "white";
                break;
            case Quality.rare:
                color = "yellow";
                break;
            case Quality.uncommon:
                color = "lime";
                break;
            case Quality.epic:
                color = "magenta";
                break;
            case Quality.fantastic:
                color = "red";
                break;
            
        }

        if (health > 0)
        {
            stat += "\n" + health.ToString() + " health";
        }

        if (mana > 0)
        {
            stat += "\n" + mana.ToString() + " mana";
        }

        if (radation > 0)
        {
            stat += "\n" + radation.ToString() + " radation";
        }

        if (water > 0)
        {
            stat += "\n" + water.ToString() + " water";
        }

        return string.Format("<color=" + color +"><size=16>{0}</size></color>" +
                             "<size=14>" +
                             "<color=white><i>" + newLine + "{1}</i></color>" +
                            "{2}</size>",
                             itemName, description, stat);


    }

    void addStat()
    {

        if (health != 0)
        {
            player.GetComponent<PlayerStat>().currentHealth  = (player.GetComponent<PlayerStat>().currentHealth + health >= 100)? 100 : player.GetComponent<PlayerStat>().currentHealth + health;

            player. GetComponent<PlayerStat>().calculate(player.GetComponent<PlayerStat>().healthBar, player.GetComponent<PlayerStat>().currentHealth);
        }

        if (water != 0)
        {
            player.GetComponent<PlayerStat>().currentWater = (player.GetComponent<PlayerStat>().currentWater + water >= 100) ? 100 : player.GetComponent<PlayerStat>().currentWater + water;

            player.GetComponent<PlayerStat>().calculate(player.GetComponent<PlayerStat>().waterBar, player.GetComponent<PlayerStat>().currentWater);
        }

        if (radation != 0)
        {
            player.GetComponent<PlayerStat>().currentRadiation = (player.GetComponent<PlayerStat>().currentRadiation + radation >= 100) ? 100 : player.GetComponent<PlayerStat>().currentRadiation + radation;

            player.GetComponent<PlayerStat>().calculate(player.GetComponent<PlayerStat>().radBar, player.GetComponent<PlayerStat>().currentRadiation);
        }


    }




}
