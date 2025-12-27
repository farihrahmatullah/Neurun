using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{

    public float despawnX = -15f;       // posisi X batas hilang (misalnya kiri layar)

    private bool isMoving = false;

    void OnEnable()
    {
        // Saat diaktifkan dari pool, tanah mulai bergerak
        isMoving = true;
    }

    void OnDisable()
    {
        // Reset biar aman kalau dipakai ulang
        isMoving = false;
    }

    void Update()
    {
        if (!isMoving) return;

        // Gerakkan tanah ke kiri
        float kecepatan = KecGlobal.Instance.GetKecepatanSaatIni();
        transform.Translate(Vector3.left * kecepatan * Time.deltaTime, Space.World);

        // Jika sudah melewati batas, nonaktifkan (balik ke pool)
        if (transform.position.x < despawnX)
        {
            gameObject.SetActive(false);
        }
    }
}


