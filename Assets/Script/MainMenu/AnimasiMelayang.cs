using UnityEngine;

public class AnimasiMelayang : MonoBehaviour
{
    public float floatAmplitude = 0.5f; // Seberapa tinggi objek melayang
    public float floatFrequency = 1f;   // Seberapa cepat objek melayang

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Gerakan naik-turun dengan sinus
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
