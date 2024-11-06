using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaymarkerController : MonoBehaviour
{
    private Vector3 currentObjective;
    public ObjectiveController objectiveController;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentObjective = objectiveController.CurrentObjective();
        PointToObjective(currentObjective);
    }

    void PointToObjective(Vector3 currentObjective)
    {
        Vector3 relativePos = currentObjective - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1), relativePos);
    }
}
