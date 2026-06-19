using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSounds : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void OnPointerEnter(PointerEventData eventData)      //hover
    {
        audioSource.PlayOneShot(hoverSound);
    }

    public void PlayClickSound()                //Click
    {
        audioSource.PlayOneShot(clickSound);
    }
}
