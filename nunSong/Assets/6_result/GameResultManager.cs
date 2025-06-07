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
    }
}