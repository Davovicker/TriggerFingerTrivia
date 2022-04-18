using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class P4HealthBar : MonoBehaviour
{
    //PROSPECTOR PETE, POW, PLAYER 4

    public Slider P4slider;
    public Gradient P4Gradient;
    public Image P4Fill;

    // Start is called before the first frame update
    public void SetMaxHealth(int health)
    {
        P4slider.maxValue = health;

        P4slider.value = health;

        P4Fill.color = P4Gradient.Evaluate(1f);

    }

    public void SetHealth(int health)
    {
        P4slider.value = health;

        P4Fill.color = P4Gradient.Evaluate(P4slider.normalizedValue);

    }
}
