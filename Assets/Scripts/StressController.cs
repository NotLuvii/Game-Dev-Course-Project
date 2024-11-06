using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StressController : MonoBehaviour
{
    // Start is called before the first frame update4
    public SpriteRenderer WayPoint;
    public Image Spoon1;
    public Image Spoon2;
    public Image Spoon3;
    public GameObject Fog;
    public GameObject maskObj;
    public StoryController storyController;
    private Transform mask;
    private Vector3 orgMaskSize;

    public Image StressBar;
    public GameObject objectives;
    private Dictionary<string, bool> hasRun = new Dictionary<string, bool>();
    private float lastStress;
    private float health;
    //Normalize mask
    private float maskSizeNorm;

    void Start()
    {
        //No Stress (NS)
        hasRun.Add("NS", false);
        //Little Stress (LS)
        hasRun.Add("LS", false);
        //Some Stress (SS)
        hasRun.Add("SS", false);
        //Stress (S)
        hasRun.Add("S", false);
        //Lots of Stress (LS)
        hasRun.Add("LoS", false);
        //Critical Stress (CS)
        hasRun.Add("CS", false);

        DontDestroyOnLoad(gameObject);
        mask = maskObj.transform;
        orgMaskSize = mask.localScale;
        lastStress = health;
        health = StressBar.fillAmount;
        Fog.SetActive(false);
        CognitiveConsequences();
        PrintText("What a fun day to get ready for a party.", "NS");
    }

    // Update is called once per frame
    void Update()
    {
        health = StressBar.fillAmount;

        VisualConsequences();
        CognitiveConsequences();
        LoseSpoons();


        lastStress = health;

    }
    void LoseSpoons()
    {

        Spoon3.color = new Color(1, 1, 1, health / .33f);
        Spoon2.color = new Color(1, 1, 1, (health - .33f) / .33f);

        Spoon1.color = new Color(1, 1, 1, (health - .66f) / .33f);
    }
    void VisualConsequences()
    {

        if (health <= .33)
        {
            // fog.SetActive(true);
            LoseWay();
        }

        if (health <= .67)
        {
            LoseObjectives();

        }

        if (health <= .9)
        {
            Fog.SetActive(true);
            ClosingIn(0, .9f);

        }

        if (health > .9)
        {
            Fog.SetActive(false);
        }
    }
    void CognitiveConsequences()
    {
        if (health < .1)
        {
            PrintText("AAAAAAAAAHHHHHHHHHHHHHHHH!!!!!!!", "CS");
        }
        else if (health <= .33)
        {

            PrintText("AHH!", "LoS");
        }
        else if (health < .67)
        {
            PrintText("I really need to relax", "S");
        }
        else if (health <= .8)
        {
            PrintText("Just keep it cool.", "SS");


        }
        else if (health <= .9)
        {
            PrintText("Getting ready.", "LS");
        }
        else
        {
            PrintText("What a fun day to get ready for a party.", "NS");
        }
    }

    //Consequence for spoon 2
    void ClosingIn(float mn, float mx)
    {
        Vector3 newSize;
        Vector3 maxScale = new Vector3(5f, 5f, 1);
        Vector3 minScale = new Vector3(1f, 1f, 1);


        if (lastStress != health)
        {
            // .67 - f = 0 
            // (.8 - .67)/h = 1
            // h = .13
            newSize = orgMaskSize * (health - mn) / (mx - mn);
            float x = Mathf.Clamp(newSize.x, minScale.x, maxScale.x);
            float y = Mathf.Clamp(newSize.y, minScale.y, maxScale.y);
            newSize = new Vector3(x, y, 1);
            mask.localScale = newSize;
        }

    }


    //Make the waypoint phaseout
    void LoseWay()
    {
        // 
        WayPoint.color = new Color(1, 1, 1, health / .33f);
    }

    void LoseObjectives()
    {
        objectives.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, health / .67f);
    }
    void PrintText(string message, string key)
    {

        if (!hasRun[key] && Time.timeSinceLevelLoad > 3)
        {
            storyController.setNarrative(message);

            hasRun[key] = true;
        }
        foreach (string k in new List<string>(hasRun.Keys))
        {
            if (k != key)
            {
                hasRun[k] = false;
            }
        }
    }
}
