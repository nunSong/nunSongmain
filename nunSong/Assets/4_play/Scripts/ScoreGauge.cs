using UnityEngine;
using UnityEngine.UI;

public class ScoreGauge : MonoBehaviour
{
    public static ScoreGauge Instance;

    public Slider gauge;                // 슬라이더 컴포넌트
    public Image fillImage;             // 슬라이더 Fill 이미지
    public Color normalColor = Color.green;     // 기본 색상
    public Color feverColor = Color.red;        // 피버 색상

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gauge.value = 0f;
        if (fillImage == null)
        {
            Debug.LogWarning("ScoreGauge: fillImage not assigned!");
        }
        else
        {
            fillImage.color = normalColor;
        }
    }

    private void Update()
    {
        if (FeverManager.Instance == null) return;

        if (!FeverManager.Instance.IsFeverActive())
        {
            int feverPoints = FeverManager.Instance.GetFeverPoints();
            int feverThreshold = FeverManager.Instance.GetFeverThreshold();
            gauge.value = Mathf.Clamp01((float)feverPoints / feverThreshold);
            fillImage.color = normalColor;
        }
        else
        {
            float timer = FeverManager.Instance.GetFeverTimer();
            float duration = FeverManager.Instance.GetFeverDuration();
            gauge.value = Mathf.Clamp01(timer / duration);
            fillImage.color = feverColor;
        }
    }
}
