using UnityEngine;

public class JudgmentLine : MonoBehaviour
{
    public static JudgmentLine Instance;

    public float perfectRange = 50f;
    public float greatRange = 100f;
    public float goodRange = 150f;

    private void Awake()
    {
        Instance = this;
    }

    public string Judge(float distance)
    {
        if (distance <= perfectRange)
        {
            Debug.Log($"JudgementLine distance: {distance}, result: Perfect");
            return "Perfect";
        }
        else if (distance <= greatRange)
        {
            Debug.Log($"JudgementLine distance: {distance}, result: Great");
            return "Great";
        }
        else if (distance <= goodRange)
        {
            Debug.Log($"JudgementLine distance: {distance}, result: Good");
            return "Good";
        }
        else
        {
            Debug.Log($"JudgementLine distance: {distance}, result: Miss");
            return "Miss";
        }
    }
}
