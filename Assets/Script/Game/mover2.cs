using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover2 : MonoBehaviour
{

    public float despawnX = -15f;       // posisi X batas hilang (misalnya kiri layar)
    public float kectambahan = 2f;
    public Vector3 rotasiMonster = new Vector3(0, 0, 20f); // misal miring ke kiri


    private bool isMoving = false;

    void OnEnable()
    {
        // Saat diaktifkan dari pool, tanah mulai bergerak
        isMoving = true;
        transform.rotation = Quaternion.Euler(rotasiMonster);
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
        kecepatan = kecepatan * kectambahan;
        transform.Translate(Vector3.left * kecepatan * Time.deltaTime, Space.World);

        // Jika sudah melewati batas, nonaktifkan (balik ke pool)
        if (transform.position.x < despawnX)
        {
            gameObject.SetActive(false);
        }
    }
}


