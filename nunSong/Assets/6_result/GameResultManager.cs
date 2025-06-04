using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameResultManager : MonoBehaviour
{
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
    }
}