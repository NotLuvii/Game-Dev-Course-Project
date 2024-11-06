using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    // public GameObject rectangle;
    // public GameObject target;
    public Transform followTransform;
    public BoxCollider2D mapBounds;

    private float xMin, xMax, yMin, yMax;
    private float camY, camX;
    private float camOrthsize;
    private float cameraRatio;
    private Camera mainCam;
    private Vector3 smoothPos;
    public float smoothSpeed = 0.1f;

    private void Start()
    {
        // xMin = mapBounds.bounds.min.x;
        // xMax = mapBounds.bounds.max.x;
        // yMin = mapBounds.bounds.min.y;
        // yMax = mapBounds.bounds.max.y;
        // float width = xMax - xMin;
        DontDestroyOnLoad(mapBounds);
        Vector3 Bounds = getBoundDistance();
        // rectangle.GetComponent<Rect>() = new Rect(xMin, xMax, yMin, yMin + 1);
        mainCam = GetComponent<Camera>();

        camOrthsize = mainCam.orthographicSize;
        cameraRatio = (xMax + camOrthsize) / 6;
        // target.GetComponent<SpriteRenderer>().sortingOrder = 2;

    }
    private Vector3 getBoundDistance()
    {
        SpriteRenderer[] sprites = GameObject.FindObjectsOfType<SpriteRenderer>();
        xMin = float.MaxValue;
        xMax = float.MinValue;
        yMin = float.MaxValue;
        yMax = float.MinValue;

        foreach (SpriteRenderer sprite in sprites)
        {
            if (sprite.transform.position.x < xMin)
            {
                xMin = sprite.transform.position.x;
            }

            if (sprite.transform.position.x > xMax)
            {
                xMax = sprite.transform.position.x;
            }
            if (sprite.transform.position.y < yMin)
            {
                yMin = sprite.transform.position.x;
            }

            if (sprite.transform.position.x > yMax)
            {
                yMax = sprite.transform.position.x;
            }
        }
        float width = xMax - xMin;
        float height = yMax - yMin;
        mapBounds.size = new Vector2(width, height);
        // mapBounds.offset = new Vector2(xOffset, yOffset);
        return new Vector3(width, height, 0);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        camY = Mathf.Clamp(followTransform.position.y, yMin + camOrthsize, yMax - camOrthsize);
        camX = Mathf.Clamp(followTransform.position.x, xMin + cameraRatio, xMax - cameraRatio);
        smoothPos = Vector3.Lerp(this.transform.position, new Vector3(camX, camY, this.transform.position.z), smoothSpeed);
        this.transform.position = smoothPos;
        // target.transform.position = Vector3.Lerp(this.transform.position, new Vector3(camX, camY, 0), smoothSpeed);


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
        getBoundDistance();
    }
}
