using System.Collections.Generic;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    [Header("Prefab & Pool Settings")]
    public GameObject[] letterPrefab;
    public int poolSize = 10;

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;
    public float spawnInterval = 1f;        // interval awal
    public float minSpawnInterval = 0.3f;   // batas paling cepat
    public float percepatan = 0.02f;        // berapa cepat interval berkurang per detik

    [Header("Dependencies")]
    public LetterSelector letterSelector;

    private Queue<char> letterQueue = new Queue<char>();
    private Queue<GameObject> letterPool;

    private float timer;
    private float currentInterval;

    void Start()
    {
        currentInterval = spawnInterval;

        letterPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject prefab = letterPrefab[Random.Range(0, letterPrefab.Length)];
            GameObject obj = Instantiate(prefab, Vector3.one * 9999, Quaternion.identity);
            obj.SetActive(false);
            letterPool.Enqueue(obj);
        }
    }

    void Update()
    {
        // percepatan spawn (interval mengecil)
        currentInterval -= percepatan * Time.deltaTime;
        currentInterval = Mathf.Clamp(currentInterval, minSpawnInterval, spawnInterval);

        timer += Time.deltaTime;
        if (timer >= currentInterval)
        {
            SpawnLetter();
            timer = 0;
        }
    }

    void SpawnLetter()
    {
        if (letterSelector == null || letterPool.Count == 0)
            return;

        if (letterQueue.Count == 0)
        {
            var newLetters = letterSelector.GetLettersForSpawn();
            foreach (var c in newLetters)
                letterQueue.Enqueue(c);
        }

        if (letterQueue.Count > 0)
        {
            char c = letterQueue.Dequeue();
            GameObject letterObj = letterPool.Dequeue();

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            letterObj.transform.position = spawnPoint.position;
            letterObj.transform.rotation = Quaternion.identity;

            letterObj.SetActive(true);

            var gethuruf = letterObj.GetComponent<GetHuruf>();
            if (gethuruf != null)
                gethuruf.SetLetter(c);

            letterPool.Enqueue(letterObj);
        }
    }
}
