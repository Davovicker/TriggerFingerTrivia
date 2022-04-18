using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class P3HealthBar : MonoBehaviour
{
    //BANDIT BETTY, BANG, PLAYER 3

    public Slider P3slider;
    public Gradient P3Gradient;
    public Image P3Fill;

    // Start is called before the first frame update
    public void SetMaxHealth(int health)
    {
        P3slider.maxValue = health;

        P3slider.value = health;

        P3Fill.color = P3Gradient.Evaluate(1f);

    }

    public void SetHealth(int health)
    {
        P3slider.value = health;

        P3Fill.color = P3Gradient.Evaluate(P3slider.normalizedValue);

    }
}
