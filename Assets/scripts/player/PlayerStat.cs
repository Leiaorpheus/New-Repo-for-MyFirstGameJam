using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour {
    public float currentHealth = 50;
    public float MAX = 100;
    public float currentRadiation = 10;
    public float currentWater = 20;
    public float currentAP = 0;
    public Slider healthBar;
    public Image waterBar;
    public Image radBar;

    void Start()
    {
        calculate(healthBar, currentHealth);
        calculate(waterBar, currentWater);
        calculate(radBar, currentRadiation);
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




}


