using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneAnimationScript : MonoBehaviour
{
    [SerializeField] private float angleSinWavelength;
    [SerializeField] private float angleAmplitude;
    [SerializeField] private float sizeSinWavelength;
    [SerializeField] private float sizeAmplitude;
    [SerializeField] private float minimumSize;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        // THESE ARE GOOD DEFAULT VALUES
        // timer = 0f;
        // angleSinWavelength = 0.5f;
        // angleAmplitude = 10f;
        // sizeSinWavelength = 0.8f;
        // sizeAmplitude = 0.2f;
        // minimumSize = 2.8f;

        // but the animation looks better if we add some random variation
        timer = Random.Range(0f,3f);
        angleSinWavelength = Random.Range(0.4f,0.6f);
        angleAmplitude = Random.Range(9f,10f);
        sizeSinWavelength = Random.Range(0.7f,0.9f);
        sizeAmplitude = Random.Range(0.1f,0.3f);
        minimumSize = Random.Range(2.7f,2.9f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        timer += Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(timer/angleSinWavelength) * angleAmplitude);
        float scale = Mathf.Sin(timer/sizeSinWavelength) * sizeAmplitude + minimumSize;
        transform.localScale = new Vector2(scale, scale);
    }
}
