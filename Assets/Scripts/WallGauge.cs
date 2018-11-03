using UnityEngine;
using UnityEngine.UI;

public class WallGauge : MonoBehaviour
{
    private Image _gauge;
    private float _t;
    private float _maxValue;

    void Start()
    {
        _gauge = GetComponent<Image>();
        _maxValue = 1f;
        _t = 1f;
    }

    public void DecreaseValue()
    {
        _t -= 0.01f;
        float x = Mathf.Lerp(0f, _maxValue, _t);
        _gauge.fillAmount = x;
    }
}
