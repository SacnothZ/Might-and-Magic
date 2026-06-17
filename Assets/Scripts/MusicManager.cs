using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource musicSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void ChangeMusic(AudioClip clip)
    {
        if (musicSource.clip == clip)
            return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }
}
