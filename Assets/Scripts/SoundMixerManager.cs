using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mainAudioMixer;
    public static SoundMixerManager instanceAM;

    private void Awake()
    {                               //Create Globally for all scenes.
        if (instanceAM == null)
        {
            instanceAM = this;
        }
    }

    public void SetSoundVolume(float level)
    {
        if (level <= 0.0001f)
            mainAudioMixer.SetFloat("soundVolume", -80f);
        else
            mainAudioMixer.SetFloat("soundVolume", Mathf.Log10(level) * 20);
    }
    public void SetMusicVolume(float level)
    {
        if (level <= 0.0001f)
            mainAudioMixer.SetFloat("musicVolume", -80f);
        else
            mainAudioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20);
    }


}
