using UnityEngine;

public class GaugeController : MonoBehaviour
{
    [SerializeField] BGMController _bgmController;
    [SerializeField] RectTransform _rt;

    float t;
    float maxValue;

    private void Start()
    {
        maxValue = _rt.sizeDelta.x;
        t = 1f;
    }

    public void DecreaseValue()
    {
        t -= 0.01f;
        float x = Mathf.Lerp(0f, maxValue, t);
        _rt.sizeDelta = new Vector2(x, _rt.sizeDelta.y);
        if (t <= 0f) {
            _bgmController.PlayGameOverBGM();
        }
    }
}
