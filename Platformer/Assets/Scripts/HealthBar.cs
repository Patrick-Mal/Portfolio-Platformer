using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    float currentHealth;
    float maxHealth;
    GameObject playerObject;

    private void Start()
    {
        playerObject = GameObject.Find("Player");

    }

    private void Update()
    {
        currentHealth = playerObject.GetComponent<Player>().currentHealth;
        maxHealth = playerObject.GetComponent<Player>().maxHealth;
        slider.value = currentHealth/maxHealth;
    }

}
