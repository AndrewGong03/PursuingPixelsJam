using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSpawnerScript : MonoBehaviour
{
    public GameObject TransitionGray;
    public float startTime = 5; // opening scene
    public float endTime = 10; // end opening scene
    private float timer = 0; 
    // [SerializeField] private CanvasGroup TransitionGrayGroup;
    [SerializeField] private Material transparentMaterial; 

    // Start is called before the first frame update
    void Start()
    {
        // Start opening scene, startTime seconds after game starts
        Invoke("StartGray", startTime); 
        Invoke("FadeInGray", startTime);
    }

    // Update is called once per frame
    void Update()
    {
        // Every time we need to start a scene where people talk, bring up the gray background 
        // Close it when we know it's done 

        // Remove gray background for opening scene
        if (timer < endTime) 
        {
            timer = timer + Time.deltaTime; 
        }
        else 
        {
            FadeOutGray();
        }
    }

    // Fade in gray background
    public void StartGray()
    {
        Instantiate(TransitionGray, transform.position, transform.rotation);
    }

    public void FadeInGray() 
    {
        // if (TransitionGrayGroup.alpha < 1) 
        // {
        //     TransitionGrayGroup.alpha += Time.deltaTime; 
        // }

        Color color = transparentMaterial.color; 
        color.a = 1;
        transparentMaterial.color = color;
    }

    public void FadeOutGray()
    {
        Color color = transparentMaterial.color; 
        color.a = 0;
        transparentMaterial.color = color;
    }
}
