using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class DisplayInfoUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI displayName;
    [SerializeField]
    private TextMeshProUGUI description;

    public void ShowDisplay(ItemData ItemData)
    {
        displayName.text = ItemData.itemName;
        description.text = ItemData.description;
    }

    public void SetActive(bool isEnabled)
    {
        displayName.enabled = isEnabled;
        description.enabled = isEnabled;
    }
}
