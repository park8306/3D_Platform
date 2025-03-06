using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get => instance; private set { } }

    public HPUI hpUI;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        hpUI = FindObjectOfType<HPUI>();
    }

    public void SetHPUI(float hpRatio)
    {
        hpUI.SetHPLine(hpRatio);
    }
}
