using UnityEngine;
using UnityEngine.UI;

public class ScoreGauge : MonoBehaviour
{
    public static ScoreGauge Instance;
    public Slider gauge;
    public Image fillImage;
    public Color normalColor = Color.green;
    public Color feverColor = Color.red;

    private void Awake() { Instance = this; }

    private void Start()
    {
        gauge.value = 0f;
        fillImage.color = normalColor;
    }

    private void Update()
    {
        if (FeverManager.Instance == null) return;

        if (!FeverManager.Instance.IsFeverActive())
        {
            gauge.value = Mathf.Clamp01((float)FeverManager.Instance.GetFeverPoints() / FeverManager.Instance.GetFeverThreshold());
            fillImage.color = normalColor;
        }
        else
        {
            gauge.value = Mathf.Clamp01(FeverManager.Instance.GetFeverTimer() / FeverManager.Instance.GetFeverDuration());
            fillImage.color = feverColor;
        }
    }
}
