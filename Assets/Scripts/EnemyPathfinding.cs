using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{

    public GameObject[] wayMarkers;
    public AnimationClip[] allSprites;
    int currentWayMarker = 0;

    public float speed = 1f;
    private Animator anim;
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 5f)
        {
            EnemyMovement();
        }
        anim.Play(allSprites[currentWayMarker].name);
    }

    void EnemyMovement()
    {
        if (this.transform.position == wayMarkers[currentWayMarker].transform.position)
        {
            currentWayMarker++;
        }

        if (currentWayMarker >= wayMarkers.Length)
        {
            currentWayMarker = 0;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, wayMarkers[currentWayMarker].transform.position, speed * Time.deltaTime);
    }
}
