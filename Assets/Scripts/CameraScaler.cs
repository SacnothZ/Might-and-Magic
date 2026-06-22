using UnityEngine;


public class CameraScaler : MonoBehaviour
{
    Camera camera;

    float referenceAspect;
    float referenceSize;

    void Start()
    {
        camera = GetComponent<Camera>();
        referenceAspect = (float)Screen.width / Screen.height;  //screen 
        referenceSize = camera.orthographicSize;    //current orthorgraphic sizeo n camera
    }

    void Update()
    {
        float aspect = (float)Screen.width / Screen.height;

        camera.orthographicSize = referenceSize * (referenceAspect / aspect);
    }
}

