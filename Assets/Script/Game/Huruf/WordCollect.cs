using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WordCollect : MonoBehaviour
{
    public static WordCollect Instance;

    private const string SAVE_KEY = "KataTersimpan";

    // Simpan jumlah benar tiap kata
    private Dictionary<string, int> kataBenar = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress(); // 🔹 langsung load data yang tersimpan
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ✅ Tandai kata sudah diambil / benar
    public void TandaiKataSudahDiambil(string kata)
    {
        kata = kata.ToUpper();

        if (kataBenar.ContainsKey(kata))
            kataBenar[kata]++;
        else
            kataBenar[kata] = 1;

        SaveProgress();
    }

    // 🔍 Cek apakah kata sudah diambil minimal 1x
    public bool SudahDiambil(string kata)
    {
        return kataBenar.ContainsKey(kata.ToUpper());
    }

    // 🔢 Ambil jumlah benar kata tertentu
    public int GetJumlahBenar(string kata)
    {
        kata = kata.ToUpper();
        return kataBenar.ContainsKey(kata) ? kataBenar[kata] : 0;
    }

    // 💾 Simpan semua data ke PlayerPrefs
    private void SaveProgress()
    {
        string data = string.Join(";", kataBenar.Select(kvp => $"{kvp.Key}:{kvp.Value}"));
        PlayerPrefs.SetString(SAVE_KEY, data);
        PlayerPrefs.Save();
    }

    // 📥 Load data dari PlayerPrefs
    private void LoadProgress()
    {
        string data = PlayerPrefs.GetString(SAVE_KEY, "");
        if (string.IsNullOrEmpty(data)) return;

        kataBenar = data.Split(';')
                        .Select(entry => entry.Split(':'))
                        .Where(parts => parts.Length == 2)
                        .ToDictionary(parts => parts[0], parts => int.Parse(parts[1]));
    }

    // 🔄 Reset progress (opsional)
    public void ResetProgress()
    {
        kataBenar.Clear();
        PlayerPrefs.DeleteKey(SAVE_KEY);
        PlayerPrefs.Save();
    }

        public int GetTotalKataDiambil()
    {
        return kataBenar.Count;
    }
}
