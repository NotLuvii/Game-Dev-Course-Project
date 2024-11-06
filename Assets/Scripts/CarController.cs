using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//add for Random number generation
using Random = UnityEngine.Random;
public class CarController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject car;
    private float xMin, xMax, yMin, yMax;
    public Collider2D mapBounds;

    private SpriteRenderer rend;
    private Vector3 velocity;
    public float speed = 2.0f;

    private AudioSource audioSource;

    void Start()
    {
        if (car.gameObject.tag == "Left")
            velocity = new Vector3(-1f, 0f, 0f);
        if (car.gameObject.tag == "Right")
            velocity = new Vector3(1f, 0f, 0f);
        if (!car.GetComponent<SpriteRenderer>())
            car.AddComponent<SpriteRenderer>();
        rend = car.GetComponent<SpriteRenderer>();
        audioSource = gameObject.GetComponent<AudioSource>();

        xMin = -12;
        xMax = 12;
        yMin = mapBounds.bounds.min.y;
        yMax = mapBounds.bounds.max.y;
    }

    // Update is called once per frame
    void Update()
    {
        // calculate location of screen borders
        // this will make more sense after we discuss vectors and 3D
        var dist = car.transform.position.z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        //get the width of the object
        float width = rend.bounds.size.x;
        float height = rend.bounds.size.y;

        //loop car back around once it hits edge of map
        if ((transform.position.x <= xMin) && velocity.x < 0f)
        {
            transform.position = new Vector3(xMax, -0.98f, dist);
        }
        if ((transform.position.x >= xMax) && velocity.x > 0f)
        {
            transform.position = new Vector3(xMin, -1.91f, dist);
        }
        transform.position = transform.position + velocity * Time.deltaTime * speed;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    void OnTriggerExit2D(Collider2D c)
    {
        var dist = (transform.position - Camera.main.transform.position).z;
        if (c.gameObject.CompareTag("CarPath"))
        {
            if ((transform.position.x <= xMin) && velocity.x < 0f)
            {
                transform.position = new Vector3(xMin, -0.98f, dist);
            }
            if ((transform.position.x >= xMax) && velocity.x > 0f)
            {
                transform.position = new Vector3(xMin, -1.91f, dist);
            }
        }
    }
}
