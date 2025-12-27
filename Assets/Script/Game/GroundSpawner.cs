using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [Header("Prefab & Pool Settings")]
    public GameObject[] groundPrefabs;  // daftar prefab tanah
    public int poolSize = 10;           // banyak tanah siap pakai

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;     // 2 titik spawn
    public float spawnInterval = 2f;    // jeda antar spawn

    private Queue<GameObject> groundPool;
    private float timer;

    void Start()
    {
        // Buat pool
        groundPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject prefab = groundPrefabs[Random.Range(0, groundPrefabs.Length)];
            GameObject obj = Instantiate(prefab, Vector3.one * 9999, Quaternion.identity);
            obj.SetActive(false);
            groundPool.Enqueue(obj);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnGround();
            timer = 0;
        }
    }

    void SpawnGround()
    {
        if (groundPool.Count == 0) return;

        // Ambil dari pool
        GameObject ground = groundPool.Dequeue();

        // Pilih titik spawn random
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Aktifkan di titik spawn
        ground.transform.position = point.position;
        ground.transform.rotation = Quaternion.identity;
        ground.SetActive(true);

        // Masukkan balik ke pool (biar nanti bisa dipakai lagi)
        groundPool.Enqueue(ground);
    }

    public void IntervalSet()
    {
        spawnInterval -= 0.2f;

        if(spawnInterval == 1.8f)
        {
            spawnInterval = 1.8f;
        }
    }
}
