using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] private AudioClip music;

    private void Start()
    {
        MusicManager.instance.ChangeMusic(music);
    }
}
