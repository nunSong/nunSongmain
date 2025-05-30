using UnityEngine;

public class JudgmentLine : MonoBehaviour
{
    public static JudgmentLine Instance;

    // 접근 가능한 범위 값들 (인스펙터에 노출 방지)
    [HideInInspector] public readonly float perfectRange = 50f;  // ±50ms=0.05f로 추후 변경, 아래도 동일
    [HideInInspector] public readonly float greatRange = 100f;    // ±80ms
    [HideInInspector] public readonly float goodRange = 150f;     // ±120ms

    private void Awake()
    {
        Instance = this;
    }

    public string Judge(float distance)
    {
        if (distance <= perfectRange) return "Perfect";
        else if (distance <= greatRange) return "Great";
        else if (distance <= goodRange) return "Good";
        else return "Miss";
    }
}
