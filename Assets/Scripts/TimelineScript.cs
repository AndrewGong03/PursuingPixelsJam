using UnityEngine;
using UnityEngine.Playables;

public class TimelineScript : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public AudioClip cutSceneMusic;

    void Start() {
        AudioManagerScript.Instance.StopMusic();
    }
    void Update()
    {
        AudioManagerScript.Instance.PlayMusic(cutSceneMusic);
        if (playableDirector.state == PlayState.Paused && Input.GetMouseButtonDown(0))
        {
            playableDirector.Play();
        }
    }
}
