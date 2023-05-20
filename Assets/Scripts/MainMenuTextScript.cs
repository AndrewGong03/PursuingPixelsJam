using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTextScript : MonoBehaviour
{
    [SerializeField] private RectTransform textTransform;
    [SerializeField] private float ySinWavelength;
    [SerializeField] private float ySinAmplitude;
    private float timer;
    private float startingY;

    // Start is called before the first frame update
    void Start()
    {
        ySinWavelength = 0.3f;
        ySinAmplitude = 0.07f;
        timer = 0f;
        startingY = textTransform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 newTransform = new Vector3(textTransform.position.x, Mathf.Sin(timer/ySinWavelength)*ySinAmplitude + startingY, textTransform.position.z);
        textTransform.position = newTransform;
    }
}
