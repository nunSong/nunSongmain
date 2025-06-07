using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

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
        greatText.text = ScoreManager.Instance.greatCount.ToString();
        goodText.text = ScoreManager.Instance.goodCount.ToString();
        missText.text = ScoreManager.Instance.missCount.ToString();
    }

    public void OnClickGoToMainScene()
    {
        SceneManager.LoadScene("initScene");
    }

    public void OnClickRestartGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void OnClickSelectSong()
    {
        SceneManager.LoadScene("initScene");
        StartCoroutine(SetSongDetailUIActive());
    }
    
    private IEnumerator SetSongDetailUIActive()
{
    yield return new WaitForSeconds(0.1f); // 씬 로드 후 딜레이

    GameObject canvas = GameObject.Find("canvas");
    if (canvas != null)
    {
        canvas.transform.Find("IntroUI")?.gameObject.SetActive(false);
        canvas.transform.Find("mainUI")?.gameObject.SetActive(false);
        canvas.transform.Find("SettingUI")?.gameObject.SetActive(false);
        canvas.transform.Find("SelectUI")?.gameObject.SetActive(false);
        canvas.transform.Find("SongDetailUI")?.gameObject.SetActive(true);
    }
}
}