using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoaderScript : MonoBehaviour
{
    public Animator transition; 
    public float transitionTime = 1f;
    public GameObject dayTitleScreen;
    public TMP_Text dayTitleScreenText;
    public GameObject timeline;

    public AudioClip menuMusic;

    // Update is called once per frame
    void Update()
    {   
        // exception to the "click button then change scenes" for main screen 
        if (isSceneZero()) 
        {
            AudioManagerScript.Instance.PlayMusic(menuMusic);
        }

    }

    public bool isSceneZero() {
        return SceneManager.GetActiveScene().buildIndex == 0;
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadNextLevelWithTitle(string text)
    {
        StartCoroutine(LoadDayTitleScreen(text));
    }

    IEnumerator LoadDayTitleScreen(string titleText) {
        timeline.SetActive(false);
        dayTitleScreen.SetActive(true);
        dayTitleScreenText.text = titleText;
        SpriteRenderer titleScreen = dayTitleScreen.GetComponent<SpriteRenderer>();
        titleScreen.color = new Color(0,0,0,0);
        Color targetColor = new Color(0,0,0,1f);

        while (titleScreen.color.a < 0.95f) {
            Debug.Log("While loop");
            titleScreen.color = Color.Lerp(titleScreen.color,targetColor,0.8f * Time.deltaTime);
            yield return null;
        }
        
        dayTitleScreen.SetActive(false);
        dayTitleScreenText.text = "";
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex) 
    {
        transition.SetTrigger("Start"); // Play animation 

        yield return new WaitForSeconds(transitionTime); // Wait for some time

        SceneManager.LoadScene(levelIndex); // Load scene
    }
}
