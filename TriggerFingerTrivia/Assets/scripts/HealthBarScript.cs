using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    //this is a reference to the slider I think we will tie this into a score script with multiple health bars
    public Slider P1slider;
    public Slider P2slider;
    public Slider P3slider;
    public Slider P4slider;

    public void SetMaxHealth(int health)
    {
        P1slider.maxValue = health;
        P2slider.maxValue = health;
        P3slider.maxValue = health;
        P4slider.maxValue = health;

        P1slider.value = health;
        P2slider.value = health;
        P3slider.value = health;
        P4slider.value = health;
    }

    public void SetHealth(int health)
    {
        P1slider.value = health;
        P2slider.value = health;
        P3slider.value = health;
        P4slider.value = health;

    }

}
