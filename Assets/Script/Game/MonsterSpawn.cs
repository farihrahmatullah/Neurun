using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject SpawnerMonster;
    public GameObject SpawnerMonsterTerbang;
    public ScoreManager scoreManager;

    private bool hasSpawned = false;

    void Start()
    {
        SpawnerMonster.SetActive(false);
        SpawnerMonsterTerbang.SetActive(false);
    }

    void Update()
    {
        if (!hasSpawned && scoreManager.score >= 500)
        {
            hasSpawned = true;
            SpawnerMonster.SetActive(true);
            SpawnerMonsterTerbang.SetActive(true);
        }
    }
}

