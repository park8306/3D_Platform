using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    Image hpLine;

    private void Start()
    {
        hpLine = transform.GetChild(0).GetChild(0).GetComponent<Image>();
    }

    public void SetHPLine(float hpRatio)
    {
        hpLine.fillAmount = hpRatio;
    }
}
