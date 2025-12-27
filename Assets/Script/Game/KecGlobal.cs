using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KecGlobal : MonoBehaviour
{
    [Header("Kecepatan Global")]
    [SerializeField] public float kecepatanAwal = 7f;

    public static KecGlobal Instance { get; private set; }
    public Vector3 direction = Vector3.right; // arah gerak (kanan)
    private float jumlahbertambah = 0.2f;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // Jaga supaya hanya ada satu instance
    }

    public float GetKecepatanSaatIni()
    {
        return kecepatanAwal;
    }

    public void UpdateKecepatan()
    {
        kecepatanAwal = kecepatanAwal + jumlahbertambah;
        if ( kecepatanAwal == 6)
        {
            kecepatanAwal = 6;
        }
    }
}
