using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infinitybackground : MonoBehaviour
{
    public float speed = 3f; // Kecepatan scroll background
    public Transform cameraTransform; // Ambil Kamera utama

    public float spawnX = 15f;
    public float despawnX = -15f;   

    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        // Pindahkan background ke kiri sesuai kecepatan
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

         // Jika sudah melewati batas kiri, pindahkan ke posisi kanan
        if (transform.position.x < despawnX)
        {
            Vector3 newPos = transform.position;
            newPos.x = spawnX;
            transform.position = newPos;
        }
        
    }
}
