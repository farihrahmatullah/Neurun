using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class coingameplay : MonoBehaviour
{
    private int temporarycoin = 0;

    public TMP_Text coinText;
    public TMP_Text gameovercointext;

    // Start is called before the first frame update
    void Start()
    {
        temporarycoin = 0;
    }


    public void Tambahcoin()
    {
        temporarycoin += 1;
        coinText.text = temporarycoin.ToString();
        gameovercointext.text = temporarycoin.ToString();
    }

    public void Savecoin()
    {
        coinmanager.Instance.AddCoin(temporarycoin);
    }

    public void Resetcoin()
    {
        temporarycoin = 0;
        coinText.text = temporarycoin.ToString();
        gameovercointext.text = temporarycoin.ToString();
    }
}
