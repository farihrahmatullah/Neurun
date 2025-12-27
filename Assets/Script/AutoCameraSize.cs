using UnityEngine;

public class AutoCameraSize : MonoBehaviour
{
     public float baseSize = 5f;   // ortho size untuk HP Android (16:9)

    void Start()
    {
        
        Camera cam = GetComponent<Camera>();

        float aspect = (float)Screen.width / Screen.height;
             Debug.Log("Aspect: " + aspect);

        // kategori berdasarkan aspek rasio
        if (aspect >= 1.70f)               
        {
            // HP Android 16:9 (1.77)
            cam.orthographicSize = baseSize;  
        }
        else if (aspect >= 1.55f && aspect < 1.70f)  
        {
            // Tablet 16:10 (1.6)
            cam.orthographicSize = baseSize * 1.1f;
        }
        else if (aspect >= 1.40f && aspect < 1.55f)
        {
            // Tablet 3:2 (1.5)
            cam.orthographicSize = baseSize * 1.2f;
        }
        else if (aspect >= 1.25f && aspect < 1.40f)
        {
            // iPad 4:3 (1.33)
            cam.orthographicSize = baseSize * 1.34f;
        }
        else
        {
            // fallback untuk device lain yang sangat kotak
            cam.orthographicSize = baseSize * 1.4f;
        }
    }
}
