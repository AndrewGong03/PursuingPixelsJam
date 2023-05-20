using UnityEngine;
using UnityEngine.Playables;

public class TimelineScript : MonoBehaviour
{
    public PlayableDirector playableDirector;

    void Update()
    {
        if (playableDirector.state == PlayState.Paused && Input.GetMouseButtonDown(0))
        {
            playableDirector.Play();
        }
    }
}
