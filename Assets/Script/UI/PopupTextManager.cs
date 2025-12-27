using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTextManager : MonoBehaviour
{
    public static PopupTextManager Instance;
    public GameObject popupPrefab;
    public Canvas worldCanvas; // canvas untuk popup

    void Awake()
    {
        Instance = this;
    }

    public void Spawn(Transform target, string msg, Color color)
    {
        // Offset dasar (di atas karakter)
            Vector3 baseOffset = new Vector3(0, 1f, 0);

            // Offset acak kecil biar tidak nempel tepat di satu titik
            Vector3 randomOffset = new Vector3(
                UnityEngine.Random.Range(-0.9f, 0.9f),  // geser kanan/kiri kecil
                UnityEngine.Random.Range(0.4f, 0.8f),   // naik sedikit
                0
            );

            // Hitung posisi layar
            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position + baseOffset + randomOffset);

            // Buat popup
            var obj = Instantiate(popupPrefab, worldCanvas.transform);
            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.position = screenPos;

            // Random rotasi kecil, biar ada variasi visual
            rt.rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-15f, 15f));

            obj.GetComponent<PopupText>().Show(msg, color);
    }

    internal void Spawn(object karakterTransform, string v, Color green)
    {
        throw new NotImplementedException();
    }
}
