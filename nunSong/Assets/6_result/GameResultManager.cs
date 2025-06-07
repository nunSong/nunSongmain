using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameResultManager : MonoBehaviour
{
    [Header("Result UI")]
    [SerializeField] private GameObject[] gradeCoverCanvases;
    [SerializeField] private TMP_Text resultScoreText;

    [Header("Buttons")]
    [SerializeField] private Button goToMainButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button selectSongButton;

    [Header("Result Texts")]
    [SerializeField] private TMP_Text perfectText;
    [SerializeField] private TMP_Text greatText;
    [SerializeField] private TMP_Text goodText;
    [SerializeField] private TMP_Text missText;

    void Start()
    {
        // 버튼에 이벤트 연결
        goToMainButton.onClick.AddListener(() =>
        {
            GameManager.Instance.OnClickGoToMainScene();
        });

        restartButton.onClick.AddListener(() =>
        {
            GameManager.Instance.OnClickRestartGame();
        });

        selectSongButton.onClick.AddListener(() =>
        {
            GameManager.Instance.OnClickSelectSongDetailFromMain();
        });

        UpdateResultUI();
    }

    private void UpdateResultUI()
    {
        int gradeIndex = GameManager.Instance.GetCurrentGradeIndex();
        for (int i = 0; i < gradeCoverCanvases.Length; i++)
            gradeCoverCanvases[i].SetActive(i == gradeIndex);

        int score = ScoreManager.Instance.GetScore();
        resultScoreText.text = score.ToString("D6");

        perfectText.text = ScoreManager.Instance.perfectCount.ToString();
        greatText.text   = ScoreManager.Instance.greatCount.ToString();
        goodText.text    = ScoreManager.Instance.goodCount.ToString();
        missText.text    = ScoreManager.Instance.missCount.ToString();
    }
}