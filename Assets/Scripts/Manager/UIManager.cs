using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get => instance; private set { } }

    public HPUI hpUI;
    public DisplayInfoUI displayInfoUI;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        hpUI = FindObjectOfType<HPUI>();
        displayInfoUI = FindObjectOfType<DisplayInfoUI>();

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
}
