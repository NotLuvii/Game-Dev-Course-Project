using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject lamp;
    public GameObject Canvas;
    public GameObject FollowCamera;
    private bool thit;
    private bool bhit;
    private bool lhit;
    private bool rhit;
    private Vector3 velocity;

    private SpriteRenderer rend;
    private Animator anim;
    public float speed = 1.0f;

    public ObjectiveController objectiveController;

    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(Canvas);
        DontDestroyOnLoad(FollowCamera);
        DontDestroyOnLoad(objectiveController);
        velocity = new Vector3(0f, 0f, 0f);
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        // calculate location of screen borders
        // this will make more sense after we discuss vectors and 3D
        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        //get the width of the object
        float width = rend.bounds.size.x;
        float height = rend.bounds.size.y;

        //set the direction based on  input
        //note this is a simplified version //in assignment 1 we used a the Input Handler System

        if (!Input.anyKeyDown)
        {
            velocity = new Vector3(0f, 0f, 0f);
            anim.enabled = false;

        }
        if (Input.GetKey("left") && !lhit)
        {
            anim.enabled = true;
            velocity = new Vector3(-1f, 0f, 0f);
            anim.Play("LeftChar");
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        if (Input.GetKey("right") && !rhit)
        {
            anim.enabled = true;
            velocity = new Vector3(1f, 0f, 0f);
            anim.Play("RightChar");
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        if (Input.GetKey("down") && !bhit)
        {
            anim.enabled = true;
            velocity = new Vector3(0f, -1f, 0f);
            anim.Play("DownChar");
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        if (Input.GetKey("up") && !thit)
        {
            anim.enabled = true;
            velocity = new Vector3(0f, 1f, 0f);
            anim.Play("UpChar");
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        //make sure the obect is inside the borders... if edge is hit reverse direction
        if ((transform.position.x <= leftBorder + width / 2.0) && velocity.x < 0f)
        {
            velocity = new Vector3(0f, 0f, 0f);
        }
        if ((transform.position.x >= rightBorder - width / 2.0) && velocity.x > 0f)
        {
            velocity = new Vector3(0f, 0f, 0f);
        }
        if ((transform.position.y <= bottomBorder + height / 2.0) && velocity.y < 0f)
        {
            velocity = new Vector3(0f, 0f, 0f);
        }
        if ((transform.position.y >= topBorder - height / 2.0) && velocity.y > 0f)
        {
            velocity = new Vector3(0f, 0f, 0f);
        }
        transform.position = transform.position + velocity * Time.deltaTime * speed;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Boundary"))
        {
            if (velocity.y > 0)
            {
                thit = true;
            }
            if (velocity.y < 0)
            {
                bhit = true;
            }
            if (velocity.x > 0)
            {
                rhit = true;
            }
            if (velocity.x < 0)
            {
                lhit = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        thit = false;
        bhit = false;
        rhit = false;
        lhit = false;
    }

    void OnTriggerStay2D(Collider2D c)
    {
        Color white = new Color(1, 1, 1, 1);
        if (SceneManager.GetActiveScene().buildIndex == 1 && c.CompareTag("Lamp"))
        {
            lamp.GetComponent<SpriteRenderer>().color = white;
        }

        if (c.CompareTag("Objective"))
        {
            objectiveController.RemoveObjective(c.gameObject);
        }

        if (c.CompareTag("Door") && objectiveController.RemainingObjectives() == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded; //You add your method to the delegate
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }
    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 2)
        {
            transform.position = new Vector3(-9.7f, -3.107332f, 0f);
        }
        if (scene.buildIndex == 3)
        {
            transform.position = new Vector3(-23.24f, -15.29f, 0.0f);
        }
        if (scene.buildIndex == 4)
        {
            transform.position = new Vector3(4.97f, 2.4f, 0.0f);
        }
        if (scene.buildIndex == 5)
        {
            transform.position = new Vector3(5.6f, -2.0f, 0.0f);
        }
        if (scene.buildIndex == 6)
        {
            transform.position = new Vector3(5.6f, -2.0f, 0.0f);
        }
        objectiveController.addObjectives();
    }
}