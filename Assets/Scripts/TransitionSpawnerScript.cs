using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSpawnerScript : MonoBehaviour
{
    [SerializeField] private Material transparentMaterial;
    private float fadeSpeed = 0.005f; // rate of fading
    private float halfTheScreenY = 2.13f;

    // Start is called before the first frame update
    void Start()
    {
        StartGray();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartGray()
    {
        Color transparent = transparentMaterial.color;
        transparent.a = 0f;
        transparentMaterial.color = transparent;
    }

    public IEnumerator FadeInGrayWholeScreen() 
    {
        Color color = transparentMaterial.color;
        while (color.a < 1) 
        {
            color.a += fadeSpeed;
            transparentMaterial.color = color;
            yield return null;
        }
    }

    public IEnumerator FadeOutGrayBottomHalf()
    {
        Vector3 targetPosition = transform.position;
        while (targetPosition.y < halfTheScreenY) {
            targetPosition.y += fadeSpeed;
            transform.position = targetPosition;
            yield return null;
        }
    }

    public IEnumerator FadeInGrayBottomHalf()
    {
        Vector3 targetPosition = transform.position;
        while (targetPosition.y > 0) {
            targetPosition.y -= fadeSpeed;
            transform.position = targetPosition;
            yield return null;
        }
    }

    public IEnumerator FadeOutGrayWholeScreen()
    {
        Color color = transparentMaterial.color; 
        while (color.a > 0)
        {
            color.a -= fadeSpeed;
            transparentMaterial.color = color;
            yield return null;
        }
    }
    
    
}