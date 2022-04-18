using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class P2HealthBar : MonoBehaviour
{
    //DASTARDLY DAVE, DRAW, PLAYER 2

    public Slider P2slider;
    public Gradient P2Gradient;
    public Image P2Fill;

    // Start is called before the first frame update
    public void SetMaxHealth(int health)
    {
        P2slider.maxValue = health;

        P2slider.value = health;

        P2Fill.color = P2Gradient.Evaluate(1f);


    }

    public void SetHealth(int health)
    {
        P2slider.value = health;

        P2Fill.color = P2Gradient.Evaluate(P2slider.normalizedValue);

    }
}
