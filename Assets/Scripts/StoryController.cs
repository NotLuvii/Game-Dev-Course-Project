using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class StoryController : MonoBehaviour
{
    // Start is called before the first frame update
    private string narrative;
    public TextMeshProUGUI story;
    private float timeLapse;
    private Coroutine cor;

    bool coRun;
    void Start()
    {
        coRun = false;
        narrative = "";
        timeLapse = 0.1f;
        // StartCoroutine(BuildText());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setNarrative(string localNarrative)
    {
        narrative = localNarrative;


        if (!coRun)
        {
            story.text = "";
            StartCoroutine(BuildText());
        }


    }
    public IEnumerator BuildText()
    {
        setFlag(true);

        for (int i = 0; i < narrative.Length; i++)
        {
            story.text += narrative[i];
            //Wait a certain amount of time, then continue with the for loop

            yield return new WaitForSeconds(timeLapse);
        }
        setFlag(false);
    }

    public void setFlag(bool a)
    {
        coRun = a;
    }
}
