using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class IngatanManager : MonoBehaviour
{
    [Header("JSON Data")]
    public TextAsset jsonLatin;
    public TextAsset jsoninggris;
    public TextAsset jsonHiragana;
    public TextAsset jsonKatakana;

    [Header("UI Components")]
    public Button btnLatin, btninggris, btnHiragana, btnKatakana;
    public Transform panelKategori;
    public Transform panelKata;
    public GameObject kategoriButtonPrefab;
    public GameObject kataItemPrefab;
    public TMP_Text kategoriTitleText; // 🔹 TMP untuk menampilkan kategori aktif

    [Header("Button Colors (Custom)")]
    public Color normalColor = Color.white;
    public Color selectedColor = new Color(0.6f, 0.8f, 1f); // warna aktif (bisa ubah di inspector)
    public TMP_Text globalCounterText;


    public int totalDiambil;


    private List<WordData> dataAktif = new List<WordData>();
    private Button lastSelectedCategoryButton = null;

    void Start()
    {
        btnLatin.onClick.AddListener(() => {
        AudioManager.instance.PlaybuttonClickSFX();
        LoadData(jsonLatin, btnLatin);
    });

     btninggris.onClick.AddListener(() => {
        AudioManager.instance.PlaybuttonClickSFX();
        LoadData(jsoninggris, btninggris);
    });

    btnHiragana.onClick.AddListener(() => {
        AudioManager.instance.PlaybuttonClickSFX();
        LoadData(jsonHiragana, btnHiragana);
    });

    btnKatakana.onClick.AddListener(() => {
        AudioManager.instance.PlaybuttonClickSFX();
        LoadData(jsonKatakana, btnKatakana);
    });


        // 🔹 Default: Latin
        LoadData(jsonLatin, btnLatin);
        UpdateGlobalCounter();

    }

    void LoadData(TextAsset jsonFile, Button activeButton)
    {

        if (jsonFile == null) return;

        ClearPanel(panelKategori);
        ClearPanel(panelKata);

        var data = JsonUtility.FromJson<WordList>(jsonFile.text);
        dataAktif = data.words;

        var kategoriUnik = dataAktif.Select(d => d.kategori).Distinct().ToList();

        foreach (string kategori in kategoriUnik)
        {
            GameObject btnObj = Instantiate(kategoriButtonPrefab, panelKategori);
            TMP_Text label = btnObj.GetComponentInChildren<TMP_Text>();
            label.text = kategori;

            Button categoryButton = btnObj.GetComponent<Button>();
            string captured = kategori;

            categoryButton.onClick.AddListener(() =>
            {
                TampilkanKata(captured);
                UpdateCategoryButtonColor(categoryButton);
                UpdateKategoriTMP(captured);
            });
        }

        // 🔹 Otomatis tampil kategori pertama
        if (kategoriUnik.Count > 0)
        {
            TampilkanKata(kategoriUnik[0]);
            UpdateKategoriTMP(kategoriUnik[0]);
        }
    }

    void TampilkanKata(string kategori)
    {
        AudioManager.instance.PlaybuttonClickSFX();
        ClearPanel(panelKata);

        var kataDalamKategori = dataAktif.Where(k => k.kategori == kategori);

        foreach (var kata in kataDalamKategori)
        {
            GameObject item = Instantiate(kataItemPrefab, panelKata);
            SetupKataItem(item, kata);
        }
    }

    void SetupKataItem(GameObject item, WordData kata)
    {
        TMP_Text txtKata = item.transform.Find("Kata")?.GetComponent<TMP_Text>();
        TMP_Text txtArti = item.transform.Find("Arti")?.GetComponent<TMP_Text>();
        TMP_Text txtPoint = item.transform.Find("Point")?.GetComponent<TMP_Text>();

        bool sudahDiambil = WordCollect.Instance != null && WordCollect.Instance.SudahDiambil(kata.word);
        int jumlahBenar = WordCollect.Instance != null ? WordCollect.Instance.GetJumlahBenar(kata.word) : 0;

        if (txtKata != null)
            txtKata.text = sudahDiambil ? kata.word : "?????";

        if (txtArti != null)
            txtArti.text = sudahDiambil ? kata.arti : "";

        if (txtPoint != null)
            txtPoint.text = jumlahBenar.ToString();
    }

    void ClearPanel(Transform panel)
    {
        foreach (Transform child in panel)
            Destroy(child.gameObject);
    }

    void UpdateCategoryButtonColor(Button newButton)
    {
        if (lastSelectedCategoryButton != null)
            SetButtonImageColor(lastSelectedCategoryButton, normalColor);

        SetButtonImageColor(newButton, selectedColor);
        lastSelectedCategoryButton = newButton;
    }

    void ResetCategoryButtonColor()
    {
        if (lastSelectedCategoryButton != null)
        {
            SetButtonImageColor(lastSelectedCategoryButton, normalColor);
            lastSelectedCategoryButton = null;
        }
    }

    void SetButtonImageColor(Button btn, Color color)
    {
        // 🔹 ubah warna Image background tombol, bukan ColorBlock Unity
        if (btn != null && btn.image != null)
            btn.image.color = color;
    }

    void UpdateKategoriTMP(string kategori)
    {
        if (kategoriTitleText != null)
            kategoriTitleText.text = kategori;
    }

    int HitungTotalSemuaKata()
    {
        int total = 0;

        total += JsonUtility.FromJson<WordList>(jsonLatin.text).words.Count;
        total += JsonUtility.FromJson<WordList>(jsoninggris.text).words.Count;
        total += JsonUtility.FromJson<WordList>(jsonHiragana.text).words.Count;
        total += JsonUtility.FromJson<WordList>(jsonKatakana.text).words.Count;

        return total;
    }

       
    void UpdateGlobalCounter()
    {
        if (globalCounterText == null) return;

        int totalSemua = HitungTotalSemuaKata();
        totalDiambil = HitungTotalKataYangSudahDiambil();

        globalCounterText.text = $"{totalDiambil} / {totalSemua}";

          // 🔹 Laporkan achievement kata unik
          AchievementEventHub.Report("words_unik", totalDiambil);
    }

        int HitungTotalKataYangSudahDiambil()
    {
        if (WordCollect.Instance == null) return 0;
        return WordCollect.Instance.GetTotalKataDiambil();
    }


}
