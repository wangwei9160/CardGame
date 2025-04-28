using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAspectRatio : MonoBehaviour
{
    public float targetWidth = 1920f;
    public float targetHeight = 1080f;
    public bool letterbox = true;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        UpdateCamera();
    }

    void Update()
    {
        UpdateCamera();
    }

    void UpdateCamera()
    {
        float targetRatio = targetWidth / targetHeight;
        float currentRatio = (float)Screen.width / Screen.height;

        if (letterbox)
        {
            if (currentRatio >= targetRatio)
            {
                float scale = targetHeight / Screen.height;
                float newWidth = scale * Screen.width;
                cam.rect = new Rect((1f - targetWidth / newWidth) / 2f, 0, targetWidth / newWidth, 1f);
            }
            else
            {
                float scale = targetWidth / Screen.width;
                float newHeight = scale * Screen.height;
                cam.rect = new Rect(0, (1f - targetHeight / newHeight) / 2f, 1f, targetHeight / newHeight);
            }
        }
        else
        {
            if (currentRatio >= targetRatio)
            {
                float scaleHeight = currentRatio / targetRatio;
                cam.orthographicSize = targetHeight / 200f * scaleHeight;
            }
            else
            {
                cam.orthographicSize = targetHeight / 200f;
            }
        }
    }
}