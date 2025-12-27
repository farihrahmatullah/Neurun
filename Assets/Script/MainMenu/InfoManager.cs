using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    public GameObject contenthiragana;
    public GameObject contentkatakana;
    public GameObject Tutorial;
    public GameObject gojuonhiragana;
    public GameObject dakutenhiragana;
    public GameObject yoonhiragana;
    public GameObject gojuonkatakana;
    public GameObject dakutenkatakana;
    public GameObject yoonkatakana;
    public GameObject choonpu;

    void Start()
    {
        contenthiragana.SetActive(false);
        contentkatakana.SetActive(false);
        gojuonhiragana.SetActive(false);
        dakutenhiragana.SetActive(false);
        yoonhiragana.SetActive(false);
        gojuonkatakana.SetActive(false);
        dakutenkatakana.SetActive(false);
        yoonkatakana.SetActive(false);
        choonpu.SetActive(false);
        // Tutorial.SetActive(false);
        
    }

    public void btncaramain()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        contenthiragana.SetActive(false);
        contentkatakana.SetActive(false);
        gojuonhiragana.SetActive(false);
        dakutenhiragana.SetActive(false);
        yoonhiragana.SetActive(false);
        gojuonkatakana.SetActive(false);
        dakutenkatakana.SetActive(false);
        yoonkatakana.SetActive(false);
        choonpu.SetActive(false);
    }

    public void btnhiragana()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        contenthiragana.SetActive(true);
        contentkatakana.SetActive(false);
        gojuonhiragana.SetActive(true);
        dakutenhiragana.SetActive(false);
        yoonhiragana.SetActive(false);
        gojuonkatakana.SetActive(false);
        dakutenkatakana.SetActive(false);
        yoonkatakana.SetActive(false);
        choonpu.SetActive(false);
    }

    public void btnkatakana()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        contenthiragana.SetActive(false);
        contentkatakana.SetActive(true);
        gojuonhiragana.SetActive(false);
        dakutenhiragana.SetActive(false);
        yoonhiragana.SetActive(false);
        gojuonkatakana.SetActive(true);
        dakutenkatakana.SetActive(false);
        yoonkatakana.SetActive(false);
        choonpu.SetActive(false);
    }

    public void btngojuonhiragana()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        gojuonhiragana.SetActive(true);
        dakutenhiragana.SetActive(false);
        yoonhiragana.SetActive(false);
    }

    public void btndakutenhiragana()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        gojuonhiragana.SetActive(false);
        dakutenhiragana.SetActive(true);
        yoonhiragana.SetActive(false);
    }

    public void btnyoonhiragana()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        gojuonhiragana.SetActive(false);
        dakutenhiragana.SetActive(false);
        yoonhiragana.SetActive(true);
    }

    public void btngojuonkatakana()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        gojuonkatakana.SetActive(true);
        dakutenkatakana.SetActive(false);
        yoonkatakana.SetActive(false);
        choonpu.SetActive(false);
    }

    public void btndakutenkatakana()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        gojuonkatakana.SetActive(false);
        dakutenkatakana.SetActive(true);
        yoonkatakana.SetActive(false);
        choonpu.SetActive(false);
    }

    public void btnyoonkatakana()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        gojuonkatakana.SetActive(false);
        dakutenkatakana.SetActive(false);
        yoonkatakana.SetActive(true);
        choonpu.SetActive(false);
    }
    public void btnchoonpukatakana()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        gojuonkatakana.SetActive(false);
        dakutenkatakana.SetActive(false);
        yoonkatakana.SetActive(false);
        choonpu.SetActive(true);
    }

    public void btntutorial()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        Tutorial.SetActive(true);
    }
}
