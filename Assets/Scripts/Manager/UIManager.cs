using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get => instance; private set { } }

    public HPUI hpUI;
    public DisplayInfoUI displayInfoUI;
    public GameObject victoryUI;
    public GameObject defeatUI;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        hpUI = FindObjectOfType<HPUI>();
        displayInfoUI = FindObjectOfType<DisplayInfoUI>();
        victoryUI = transform.Find("VictoryUI").gameObject;
        defeatUI = transform.Find("DefeatUI").gameObject;

        victoryUI.SetActive(false);
        defeatUI.SetActive(false);
        displayInfoUI.SetActive(false);
    }

    public void SetHPUI(float hpRatio)
    {
        hpUI.SetHPLine(hpRatio);
    }

    public void ShowItemInfo(ItemData itemData)
    {
        displayInfoUI.SetActive(true);
        displayInfoUI.ShowDisplay(itemData);
    }

    internal void DisableItemInfo()
    {
        displayInfoUI.SetActive(false);
    }

    public void VictoryUIActive()
    {
        if (!victoryUI.activeInHierarchy)
        {
            Time.timeScale = 0f;
            victoryUI.SetActive(true);
        }
    }

    public void DefeatUIActive()
    {
        if (!defeatUI.activeInHierarchy)
        {
            Time.timeScale = 0f;
            defeatUI.SetActive(true);
        }
    }

    public void RetryBtn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void EndBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
