using UnityEngine;

public class GaugeController : MonoBehaviour
{
    [SerializeField] BgmController _bgmController;
    [SerializeField] RectTransform _rt;

    float t;
    float maxValue;
    RectTransform rt;

    void Start()
    {
        maxValue = rt.sizeDelta.x;
        t = 1f;
    }

    public void DecreaseValue()
    {
        t -= 0.01f;
        var x = Mathf.Lerp(0f, maxValue, t);
        rt.sizeDelta = new Vector2(x, rt.sizeDelta.y);
        if (t <= 0f) {
            _bgmController.PlayGameOverBgm();
        }
    }
}
