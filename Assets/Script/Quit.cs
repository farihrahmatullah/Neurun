using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{

    public GameObject panelKeluar;
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Tampilkan panel konfirmasi keluar
            panelKeluar.SetActive(true);
        }
    }

    public void TombolYaKeluar()
    {
        Application.Quit();
    }

    public void TombolBatal()
    {
        panelKeluar.SetActive(false);
    }

}
