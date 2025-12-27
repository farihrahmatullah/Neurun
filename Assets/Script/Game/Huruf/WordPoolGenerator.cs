using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordPoolGenerator : MonoBehaviour
{
    [Header("Referensi")]
    public ScoreManager scoreManager;

    [Header("Filter dan Pengaturan")]
    public string kategoriFilter;
    public int currentScore;
    public int poolSize = 30;
    public bool useRandomOrder = true;

    // Simpan hasil pool di sini
    public List<WordData> wordPool;
    private List<WordData> masterList; 


    private void Start()
    {
       
    }

    public void GeneratePool()
    {
        currentScore = scoreManager.score;
        Debug.Log("Generate Pool");

        if (LoadingJSON.Instance == null || LoadingJSON.Instance.AllWords == null)
            return;

        var allWords = LoadingJSON.Instance.AllWords;


        if (currentScore <= 1000){
        allWords = allWords.Where(w => w.word.Length <= 5).ToList();
        }
        if (currentScore <= 3000){
        allWords = allWords.Where(w => w.word.Length <= 7).ToList();
        }
        else{
        allWords = allWords.Where(w => w.word.Length <= 11).ToList();
        }
        // Simpan master list
        masterList = allWords;


            wordPool = masterList
        .OrderBy(w => Random.value)
        .Take(poolSize)
        .ToList();
        
        DebugWordPool();
    }


    private void UpdateMasterListByScore()
    {
        int score = scoreManager.score;
        var allWords = LoadingJSON.Instance.AllWords;

        if (score <= 1000)
            masterList = allWords.Where(w => w.word.Length <= 5).ToList();

        else if (score <= 3000)
            masterList = allWords.Where(w => w.word.Length <= 7).ToList();

        else
            masterList = allWords.Where(w => w.word.Length <= 11).ToList();
    }



    public WordData GetRandomWord()
    {
        if (wordPool == null || wordPool.Count == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, wordPool.Count);
        return wordPool[randomIndex];
    }

    public void RemoveWord(WordData wordToRemove)
    {
        if (wordPool == null || wordPool.Count == 0) return;
        if (wordToRemove == null) return;

        if (wordPool.Contains(wordToRemove))
            wordPool.Remove(wordToRemove);

         // Pastikan masterList mengikuti skor terbaru
        UpdateMasterListByScore();
    

        // Tambahkan kata baru RANDOM dari masterList
        if (masterList != null && masterList.Count > 0)
        {
            WordData randomNewWord = masterList[Random.Range(0, masterList.Count)];
            wordPool.Add(randomNewWord);
        }
        else
        {
            // masterList kosong = regenerasi
            GeneratePool();
        }

        DebugWordPool();
    }



    public void DebugWordPool()
    {
        if (wordPool == null || wordPool.Count == 0)
        {
            Debug.Log("[WordPool] Pool kosong");
            return;
        }

        string list = string.Join(", ", wordPool.Select(w => w.word));
        Debug.Log("[WordPool] Isi Pool: " + list);
    }

    
}
