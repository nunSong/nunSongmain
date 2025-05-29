using UnityEngine;
using UnityEngine.UI;

public class ComboGauge : MonoBehaviour
{
    public static ComboGauge Instance;
    public Slider gauge;
    public float increment = 0.05f;  // 콤보당 증가량 (추후 조정 가능)

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (ComboManager.Instance != null)
        {
            // 콤보에 따라 게이지 증가 (Time.deltaTime을 곱해 초당 자연스러운 증가)
            gauge.value = Mathf.Min(gauge.value + ComboManager.Instance.CurrentCombo * increment * Time.deltaTime, 1f);

            // 피버타임 발동 조건은 FeverManager에서 확인
        }
    }
}
