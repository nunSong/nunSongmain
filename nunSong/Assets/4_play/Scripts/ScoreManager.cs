using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    private int score = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore(string result)
    {
        int baseScore = 0;
        switch (result)
        {
            case "Perfect": baseScore = 1000; break;
            case "Great": baseScore = 700; break;
            case "Good": baseScore = 300; break;
            case "Miss": baseScore = 0; break;
        }

        int finalScore = FeverManager.Instance.IsFeverActive() ? baseScore * 2 : baseScore;
        score += finalScore;
        scoreText.text = score.ToString();

        FeverManager.Instance.AddFeverPoints(baseScore);

        if (result != "Miss")
        {
            ComboManager.Instance.IncreaseCombo();
        }
        else
        {
            ComboManager.Instance.ResetCombo();
        }
    }

    public int GetScore() => score;

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "0";
    }
}
