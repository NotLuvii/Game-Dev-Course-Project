using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image StressBar;
    public float health = 100f;

    private float multiplier = 1;
    public ObjectiveController objectiveController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeMultiplier(float changedMultiplier)
    {
        multiplier = changedMultiplier;
    }

    public void stress(float anxietyLevel)
    {
        health -= anxietyLevel * multiplier;
        health = Mathf.Clamp(health, 0, 100);
        StressBar.fillAmount = health / 100f;

    }
    public void relax(float relaxLevel)
    {
        health += relaxLevel;
        health = Mathf.Clamp(health, 0, 100);
        StressBar.fillAmount = health / 100f;
    }
    void OnTriggerStay2D(Collider2D c)
    {
        //set up damage
        if (c.CompareTag("Kitchen"))
        {
            stress(0.1f);
        }
        if (c.CompareTag("Car Noise"))
        {
            stress(0.4f);
        }
        if (c.CompareTag("Door"))
        {
            if (objectiveController.RemainingObjectives() != 0)
            {
                stress(0.3f);
            }
        }
        if (c.CompareTag("Path"))
        {
            if (objectiveController.RemainingObjectives() != 0)
            {
                stress(0.01f);
            }
        }

        if (c.CompareTag("OuterEnemy"))
        {
           stress(0.05f);
        }

        if (c.CompareTag("InnerEnemy"))
        {
            stress(0.1f);
        }

        if (c.CompareTag("TV"))
        {
            relax(0.05f);
        }
    }
}
