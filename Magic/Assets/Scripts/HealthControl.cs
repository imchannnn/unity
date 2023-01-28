using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : MonoBehaviour
{
    public Slider slider;
    public Text healthText;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        healthText.text = "Health: "+health.ToString();
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        healthText.text = "Health: " + health.ToString();
    }

}
