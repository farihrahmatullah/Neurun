using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class PointManager : MonoBehaviour
{
    [Header("Referensi")]
    public TMP_Text pointText;              // UI TMP Text untuk poin saat ini
    public TMP_Text temporaryscoretext;
    public TMP_Text bestPointText;          // UI TMP Text untuk best point
    public TMP_Text pointGameOverText;

    public WordProgressManager wordProgress;
    public NotifManager notifManager;
    public GamePlay gameplay;
    public KecGlobal kecglobal;
    public coingameplay coinGameplay;
    public GroundSpawner monsterSpawn;
    public Transform karakterTransform;
    public Transform pointPopupSpawnTransform;

    [Header("Pengaturan Poin")]
    public int Point = 0;
    public int BestPoint = 0;
    public int correctPoint = 20;
    public int wrongPoint = -10;
    public int temporaryscore = 0;
    private bool adaHurufSalah = false;
    private List<bool> correctnessList = new List<bool>();
    private List<char> collectedLetters = new List<char>();

    private string hexCorrect = "#ffffffff";
    private string hexWrong = "#5d06ffff";

    private string hexNeutral = "#fff700ff";
    private string hasil;

     // 🧠 Tambahan untuk sistem achievement
    private int totalWordsCompleted = 0;
    private HashSet<string> uniqueWords = new HashSet<string>();

    void Start()
    {
        // Ambil data best point tersimpan
        BestPoint = PlayerPrefs.GetInt("BestPoint", 0);

        if (wordProgress != null)
        {
             wordProgress.OnLetterCollected += OnLetterCollected;
            //  wordProgress.OnLetterChecked += OnLetterChecked; // ← baru
        }
         

            
            

        UpdatePointUI();
    }

    void OnDestroy()
    {
        if (wordProgress != null)
        {
              wordProgress.OnLetterCollected -= OnLetterCollected;
                //  wordProgress.OnLetterChecked -= OnLetterChecked; // ← baru
        }
          
    }

        // }
                

        private void OnLetterCollected(char collectedLetter)
        {
            char upper = char.ToUpper(collectedLetter);
            collectedLetters.Add(upper);

            string collectedWord = new string(collectedLetters.ToArray());

            string targetWord = gameplay.currentWord.word.ToUpper();
            int index = collectedLetters.Count - 1;

            // === Cek huruf di sini saja ===
            if (upper == targetWord[index])
            {
                temporaryscore += 4;
                Color cTrue;
                ColorUtility.TryParseHtmlString(hexCorrect, out cTrue);
                PopupTextManager.Instance.Spawn(karakterTransform, "+4", cTrue);

            }
            else
            {
                temporaryscore -= 10;
                Color cFalse;
                ColorUtility.TryParseHtmlString(hexWrong, out cFalse);
                PopupTextManager.Instance.Spawn(karakterTransform, "-10", cFalse);

                hasil = collectedWord;
                StartCoroutine(NotifFalseWord());

                    Point += temporaryscore;
                    if (Point < 0) Point = 0;  // jaga biar nggak negatif

                 // 3. Bereskan
                    collectedLetters.Clear();
                    UpdatePointUI();
                    gameplay.NextWord();
                    Invoke("resettemporaryscore", 1f);


            }

            // Update UI score sementara
            if (temporaryscoretext != null)
                temporaryscoretext.text = temporaryscore.ToString();

                if (collectedLetters.Count == targetWord.Length)
                {


                    // 2. Tentukan true/false
                    if (!adaHurufSalah && collectedWord == targetWord)
                    {
                        // Semua benar → override temporaryscore ke correctPoint
                        temporaryscore += 10;
            

                        hasil = collectedWord;
                        StartCoroutine(NotifTrueWord());
                        kecglobal.UpdateKecepatan();
                        monsterSpawn.IntervalSet();

                        totalWordsCompleted++;
                        AchievementEventHub.Report("words_completed", totalWordsCompleted);

                        WordCollect.Instance.TandaiKataSudahDiambil(collectedWord);

                        if (uniqueWords.Add(collectedWord))
                            AchievementEventHub.Report("words_unik", uniqueWords.Count);
                        
                        PlayerPrefs.Save();
                    }

                    // 1. Hitung poin sekali saja
                    Point += temporaryscore;
                    if (Point < 0) Point = 0;  // jaga biar nggak negatif
                    
                    // 3. Bereskan
                    collectedLetters.Clear();
                    UpdatePointUI();
                    gameplay.NextWord();
                    Invoke("resettemporaryscore", 1f);
                }
        }


    private void resettemporaryscore()
    {
        correctnessList.Clear(); // <-- WAJIB reset list
        temporaryscore = 0;
        adaHurufSalah = false;   // <-- reset flag
        // Update UI jika perlu
        if (temporaryscoretext != null)
            temporaryscoretext.text = temporaryscore.ToString();
    }

    IEnumerator NotifTrueWord()
    {
        yield return new WaitForSeconds(1f);
        notifManager.TrueWord(hasil);
        Color cTrue;
        ColorUtility.TryParseHtmlString(hexCorrect, out cTrue);
        PopupTextManager.Instance.Spawn(karakterTransform, "+10", cTrue);
        Color cNeutral;
        ColorUtility.TryParseHtmlString(hexNeutral, out cNeutral);
        PopupTextManager.Instance.Spawn(pointPopupSpawnTransform, "+Speed", cNeutral);
        coinGameplay.Tambahcoin();
    }

    IEnumerator NotifFalseWord()
    {
        yield return new WaitForSeconds(1f);
        notifManager.FalseWord(hasil);
    }

    private void UpdatePointUI()
    {
        // Update teks poin biasa
        if (pointText) pointText.text = Point.ToString();

        // Jika point sekarang lebih tinggi dari best point, simpan
        if (Point > BestPoint)
        {
            BestPoint = Point;
            PlayerPrefs.SetInt("BestPoint", BestPoint);
            PlayerPrefs.Save();
        }

        // ✅ Kirim event ke AchievementManager
        AchievementEventHub.Report("point", Point);

        // Update teks best point
        if (bestPointText) bestPointText.text = BestPoint.ToString();
        pointGameOverText.text = Point.ToString();
    }


    public void ResetPoints()
    {
        Point = 0;
        UpdatePointUI();
    }

    public void ResetBestPoint()
    {
        PlayerPrefs.DeleteKey("BestPoint");
        BestPoint = 0;
        UpdatePointUI();
    }
}
