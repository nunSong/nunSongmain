using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    private int score = 0;

    private readonly int perfectScore = 1000;
    private readonly int greatScore = 700;
    private readonly int goodScore = 300;
    private readonly int missScore = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore(string result)
    {
        int baseScore = result switch
        {
            "Perfect" => perfectScore,
            "Great" => greatScore,
            "Good" => goodScore,
            _ => missScore
        };

        int finalScore = FeverManager.Instance.IsFeverActive() ? baseScore * 2 : baseScore;
        score += finalScore;
        scoreText.text = score.ToString();

        FeverManager.Instance.AddFeverPoints(baseScore);
        if (result != "Miss") ComboManager.Instance.IncreaseCombo();
        else ComboManager.Instance.ResetCombo();
    }

    public int GetScore() => score;
    public void ResetScore() { score = 0; scoreText.text = "0"; }
}
