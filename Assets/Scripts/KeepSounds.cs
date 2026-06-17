using UnityEngine;

public class KeepSounds : MonoBehaviour
{

    private static KeepSounds instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


}
