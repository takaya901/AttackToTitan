using UnityEngine;
using UnityEngine.UI;

public class WallGauge : MonoBehaviour
{
    Image _gauge;
    float _t;
    float _maxValue;

    void Start()
    {
        _gauge = GetComponent<Image>();
        _maxValue = 1f;
        _t = 1f;
    }

    public void DecreaseValue()
    {
        _t -= 0.01f;
        var x = Mathf.Lerp(0f, _maxValue, _t);
        _gauge.fillAmount = x;
    }
}
