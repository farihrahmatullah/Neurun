using UnityEngine;
using System.IO;

public class SS : MonoBehaviour
{
    public KeyCode screenshotKey = KeyCode.W;
    public int superSize = 0;
    public static SS Instance;

      private void Awake()
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

    private void Update()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            Capture();
        }
    }

    public void Capture()
    {
        string timeStamp = System.DateTime.Now.ToString("yyyyMMdd-HHmmss");
        string fileName = $"Screenshot_{timeStamp}.png";

        string path;

#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_IOS || UNITY_OSX
        // Path ke Documents
        string documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        path = Path.Combine(documents, fileName);
#else
        // Platform lain fallback
        path = Path.Combine(Application.persistentDataPath, fileName);
#endif

        ScreenCapture.CaptureScreenshot(path, superSize);
        Debug.Log($"Screenshot saved: {path}");
    }
}
