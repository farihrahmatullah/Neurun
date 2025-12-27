using UnityEngine;

public class camerazoom : MonoBehaviour
{
    public Camera cam;
    public float zoomStartSize = 3f;   // Ukuran awal sebelum animasi
    public float zoomTargetSize = 5f;  // Ukuran akhir animasi
    public float zoomDuration = 1f;
    public bool autoStart = true;

    private float timer = 0f;
    private bool isZooming = false;
    private float startSize;
    private float targetSize;

    void Start()
    {
        if (autoStart)
        {
            PlayZoomFrom3To5();  // Zoom saat start game
        }
    }

    public void PlayZoomFrom3To5()
    {
        cam.orthographicSize = zoomStartSize; // RESET ke 3
        startSize = zoomStartSize;
        targetSize = zoomTargetSize;
        timer = 0f;
        isZooming = true;
    }

    void Update()
    {
        if (isZooming)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / zoomDuration);
            t = Mathf.SmoothStep(0f, 1f, t);
            cam.orthographicSize = Mathf.Lerp(startSize, targetSize, t);

            if (t >= 1f)
            {
                isZooming = false;
            }
        }
    }
}

