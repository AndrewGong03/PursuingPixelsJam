using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{
    public Animator transition; 
    public float transitionTime = 1f;

    // Update is called once per frame
    void Update()
    {   
        // exception to the "click button then change scenes" for main screen 
        // my temporary button is ugly enough, not sure if I wanted to mess with main screen for now

        if (isSceneZero()) 
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                LoadNextLevel();
            }
        }

    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex) 
    {
        transition.SetTrigger("Start"); // Play animation 

        yield return new WaitForSeconds(transitionTime); // Wait for some time

        SceneManager.LoadScene(levelIndex); // Load scene
    }

    public bool isSceneZero() {
        return SceneManager.GetActiveScene().buildIndex == 0;
    }
}
