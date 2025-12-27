using System.Collections;

public interface ILoader
{
    IEnumerator Load();       // Coroutine proses loading
    float GetProgress();      // Nilai progress (0-1)
    string GetDescription();  // Deskripsi loading saat ini
}
