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

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartGray", startTime); 
        StartCoroutine(FadeInGray());
    }

    // Update is called once per frame
    void Update()
    {
        // Every time we need to start a scene where people talk, bring up the gray background 
        // Close it when we know it's done 

        timer += Time.deltaTime;

        // fade out after the opening scene 
        if (timer > startTime && timer < endTime) 
        {
            StartCoroutine(FadeOutGray());
        }
    }

    public void StartGray()
    {
        // Instantiate(TransitionGray, transform.position, transform.rotation);
        GameObject grayInstance = Instantiate(TransitionGray, transform.position, transform.rotation);
        Material grayMat = grayInstance.GetComponent<Renderer>().material;
        Color color = grayMat.color;
        color.a = 0f; // start transparent
        grayMat.color = color;

        transparentMaterial = grayMat; // use this material for fading
    }

    IEnumerator FadeInGray() 
    {
        yield return new WaitForSeconds(startTime); // wait for startTime seconds

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
        yield return new WaitForSeconds(endTime); // wait for endTime seconds

        Color color = transparentMaterial.color; 
        while (color.a > 0 && color.a <= 1)
        {
            color.a -= fadeSpeed;
            transparentMaterial.color = color;
            yield return null;
        }
    }
}
