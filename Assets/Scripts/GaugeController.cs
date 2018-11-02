using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeController : MonoBehaviour
{
    [SerializeField] private BGMController _bgmController;

    private float t;
    private float maxValue;
    private RectTransform rt;

    private void Start()
    {
        rt = GameObject.Find("Gauge").GetComponent<RectTransform>();
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
