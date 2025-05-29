using UnityEngine;

public class JudgmentLine : MonoBehaviour
{
    public static JudgmentLine Instance;

    // Inspector에 노출하지 않고 코드에서만 고정
    private float perfectRange = 50f;
    private float greatRange = 100f;
    private float goodRange = 150f;

    private void Awake()
    {
        Instance = this;
    }

    public string Judge(float distance)
    {
        Debug.Log($"Judgement distance: {distance}");

        if (distance <= perfectRange) return "Perfect";
        else if (distance <= greatRange) return "Great";
        else if (distance <= goodRange) return "Good";
        else return "Miss";
    }
}
