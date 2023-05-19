using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSpawnerScript : MonoBehaviour
{
    public GameObject TransitionGray;
    public float startTime = 5; // opening scene
    public float endTime = 10; // length of opening scene 
    private float timer = 0; 
    [SerializeField] private Material transparentMaterial;
    private float fadeSpeed = 0.005f; // rate of fading

    public GameObject chipCorner;
    public GameObject kennyKeeper;

    public int grayFlag; // check if gray background is already active 

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartGray", startTime); 
    }

    // Update is called once per frame
    void Update()
    {
        // Every time we need to start a scene where people talk, bring up the gray background 
        // Close it when we know it's done 

        // Check if Chip and Kenny are onscreen
        bool chipOnScreen = IsOnScreen(chipCorner);
        bool kennyOnScreen = IsOnScreen(kennyKeeper);

        if (grayFlag == 0) 
        {
            if (chipOnScreen || kennyOnScreen) 
            {
                StartCoroutine(FadeInGray()); // Fade in gray if Chip or Kenny are onscreen
            } 
        }        
        else 
        {   
            if (!chipOnScreen && !kennyOnScreen) 
            {
                StartCoroutine(FadeOutGray());
            }
        }
    }

    public void StartGray()
    {
        GameObject grayInstance = Instantiate(TransitionGray, transform.position, transform.rotation);
        Material grayMat = grayInstance.GetComponent<Renderer>().material;
        Color color = grayMat.color;
        color.a = 0f; // start transparent
        grayMat.color = color;

        transparentMaterial = grayMat; // use this material for fading
    }

    IEnumerator FadeInGray() 
    {
        grayFlag = 1;

        Color color = transparentMaterial.color;
        while (color.a < 1) 
        {
            color.a += fadeSpeed;
            transparentMaterial.color = color;
            yield return null;
        }
    }

    IEnumerator FadeOutGray()
    {
        grayFlag = 0;

        Color color = transparentMaterial.color; 
        while (color.a > 0)
        {
            color.a -= fadeSpeed;
            transparentMaterial.color = color;
            yield return null;
        }
    }

    bool IsOnScreen(GameObject gameObject)
    {
        Vector3 objectPosition = Camera.main.WorldToViewportPoint(gameObject.transform.position);

        // Check if the object is within the screen boundaries
        bool isOnScreen = (objectPosition.x > 0 && objectPosition.x < 1 && objectPosition.y > 0 && objectPosition.y < 1);

        return isOnScreen;
    }
}
