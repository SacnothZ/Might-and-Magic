using JetBrains.Annotations;
using UnityEngine;

public class SoundFxManager : MonoBehaviour
{
    public static SoundFxManager instance;
    [SerializeField] private AudioSource soundFxObject;

    private void Awake()        // create globally
    {
        if (instance == null)
        {
            instance = this;
            
        }
            
    }


    public void PlaySoundFxClip(AudioClip audioclip, Transform spawnTransform, float volume)
    {
        //Spawn 
        AudioSource audioSource = Instantiate(soundFxObject, spawnTransform.position, Quaternion.identity);     // don't care about rotation
        //assign audio
        audioSource.clip = audioclip;
        //assugin volumevalue
        audioSource.volume = volume;
        //play sound
        audioSource.Play();
        //lenght of clipt, in the end destroying gameobject.
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject,clipLength);
    }




}
