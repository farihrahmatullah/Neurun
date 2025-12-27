using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Settings")]
    public int score = 0;
    public int highScore = 0;
    public float scoreSpeed = 3.8f;     // Kecepatan skor bertambah
    private float timer = 0f;

    [Header("UI References")]
    public TMP_Text scoreText;
    public TMP_Text scoreGameOverText;
    public TMP_Text highScoreText;

    [Header("Dependencies")]
    public GamePlay karakter;           // Script karakter (untuk cek apakah hidup)

    void Start()
    {
        // Ambil highscore tersimpan
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();
    }

    void Update()
    {
        // Tambah waktu dan tambah skor setiap 0.2 detik selama karakter hidup
        if (karakter != null && karakter.karakterlife)
        {
            timer += Time.deltaTime;

            if (timer >= 0.2f)
            {
                AddScore(Mathf.FloorToInt(scoreSpeed));
                timer = 0f;
            }
        }
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreUI();

        // Cek apakah skor sekarang mengalahkan highscore
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        // ✅ Kirim event ke AchievementManager
        AchievementEventHub.Report("score", score);
    }

    private void UpdateScoreUI()
    {
        if (scoreText) scoreText.text = score.ToString();
        if (highScoreText) highScoreText.text = highScore.ToString();
        scoreGameOverText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore = 0;
        UpdateScoreUI();
    }
}
