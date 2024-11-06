using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveController : MonoBehaviour
{

    private ArrayList objectives;
    private GameObject[] allObjectives;
    public TextMeshProUGUI objectiveCounter;

    private string objectiveText;

    public HealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        addObjectives();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void addObjectives()
    {
        objectives = new ArrayList();
        allObjectives = GameObject.FindGameObjectsWithTag("Objective");
        objectives.AddRange(allObjectives);
        ChangeText();
    }

    public int RemainingObjectives()
    {
        return objectives.Count;
    }

    public Vector3 CurrentObjective()
    {
        if(objectives.Count != 0){
            return ((GameObject)objectives[0]).transform.position;
        }

        //  Should be the vector of the door (exit)
        GameObject exit = GameObject.FindGameObjectWithTag("Door");
        return exit.transform.position;
    }

    public void RemoveObjective(GameObject objective)
    {
        objectives.Remove(objective);

        if(objective.transform.childCount > 0)
        {
            if (objective.transform.GetChild(0).tag == "Headphones")
            {
                healthManager.changeMultiplier(0.5f);
            }
        }
        ChangeText();
    }

    void ChangeText()
    {
        objectiveText = "";

        for (int i = 0; i < RemainingObjectives(); i++)
        {
            if(((GameObject)objectives[i]).name != "-")
            {
                objectiveText += (i + 1) + ". " + ((GameObject)objectives[i]).name + '\n';
            }
        }

        if(RemainingObjectives() == 0 || objectiveText == "")
        {
            objectiveText = "Head to exit!";
        }
        objectiveCounter.text = "Objectives Left: " + '\n' + objectiveText;
     }
}
