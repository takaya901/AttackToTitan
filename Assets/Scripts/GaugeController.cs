﻿using UnityEngine;

public class GaugeController : MonoBehaviour
{
    [SerializeField] BGMController _bgmController;
    [SerializeField] RectTransform _rt;

    float t;
    float maxValue;
    RectTransform rt;

    private void Start()
    {
        maxValue = rt.sizeDelta.x;
        t = 1f;
    }

    public void DecreaseValue()
    {
        t -= 0.01f;
        float x = Mathf.Lerp(0f, maxValue, t);
        rt.sizeDelta = new Vector2(x, rt.sizeDelta.y);
        if (t <= 0f) {
            _bgmController.PlayGameOverBGM();
        }
    }
}
