using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class P1HealthBar : MonoBehaviour
{
    // COWBOOY CARLOS, CLICK, PLAYER 1

    public Slider P1slider;
    public Gradient P1Gradient;
    public Image P1Fill;

    // Start is called before the first frame update
    public void SetMaxHealth(int health)
    {
        P1slider.maxValue = health;

        P1slider.value = health;

        P1Fill.color = P1Gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        P1slider.value = health;

        P1Fill.color = P1Gradient.Evaluate(P1slider.normalizedValue);
    }
}
