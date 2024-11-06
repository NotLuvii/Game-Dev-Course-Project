using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Car_Left;
    public GameObject Car_Right;

    void Start()
    {
        Car_Left = Instantiate(Car_Left, new Vector3(10.34f, -0.98f, 0f), Quaternion.identity);
        Car_Right = Instantiate(Car_Right, new Vector3(-7f, -1.91f, 0f), Quaternion.identity);
        // Instantiate(Car_Left, new Vector3(6.34f, -0.98f, 0f), Quaternion.identity);
        // Instantiate(Car_Right, new Vector3(-4f, -1.91f, 0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        float dist = (transform.position - Camera.main.transform.position).z;
        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        // print(left.transform.position.x);
    }

}
