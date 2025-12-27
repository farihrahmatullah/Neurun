using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skinmanager : MonoBehaviour
{
    public static Skinmanager Instance;

    [Header("Skin Data")]
    public List<SkinData> skinList;

    [Header("Player Renderers")]
    public SpriteRenderer bodyRenderer;
    public SpriteRenderer mataKananRenderer;
    public SpriteRenderer mataKiriRenderer;
    public SpriteRenderer mulutRenderer;

    public int idSkinTerpilih;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        idSkinTerpilih = PlayerPrefs.GetInt("SelectedSkinID", 0);
        AssignPlayerRenderers();
        ApplySkinByID(idSkinTerpilih);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GamePlay")
        {
            AssignPlayerRenderers();
            ApplySkinByID(idSkinTerpilih);
        }

        if (scene.name == "Skin")
        {
            AssignPlayerRenderers();
            ApplySkinByID(idSkinTerpilih);
        }

        if (scene.name == "MainMenu")
        {
            AssignPlayerRenderers();
            ApplySkinByID(idSkinTerpilih);

        }
    }

    void AssignPlayerRenderers()
    {
        bodyRenderer = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
        mataKananRenderer = GameObject.FindWithTag("Matakanan").GetComponent<SpriteRenderer>();
        mataKiriRenderer = GameObject.FindWithTag("Matakiri").GetComponent<SpriteRenderer>();
        mulutRenderer = GameObject.FindWithTag("Mulut").GetComponent<SpriteRenderer>();
    }

    public void ApplySkinByID(int skinID)
    {

        SkinData skin = skinList.Find(s => s.skinID == skinID);
        if (skin == null) return;

        bodyRenderer.sprite = skin.bodySprite;
        mataKananRenderer.sprite = skin.mataKananSprite;
        mataKiriRenderer.sprite = skin.mataKiriSprite;
        mulutRenderer.sprite = skin.mulutSprite;

    }

}
