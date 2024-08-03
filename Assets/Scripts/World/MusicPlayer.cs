using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    /*
    This script handles music sources within the application.
    Primarily this is for the main background music to play,
    combining two audio sources and transition between them over time.

    Can be used to transition to different music depending on the game scene.
    */
    public AudioSource introSource, loopSource;

    // Start is called before the first frame update
    void Start()
    {
        introSource.Play();
        loopSource.PlayScheduled(AudioSettings.dspTime + introSource.clip.length);
    }
}
