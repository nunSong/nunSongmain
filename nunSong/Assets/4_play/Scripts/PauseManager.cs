using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    public GameObject pausePopup;  // 일시정지 팝업 오브젝트
    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (pausePopup != null)
        {
            pausePopup.SetActive(isPaused);  // PausePopup 활성화/비활성화
        }

        Time.timeScale = isPaused ? 0 : 1;
    }

    public void Resume()
    {
        isPaused = false;
        if (pausePopup != null)
        {
            pausePopup.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;  // 재개 시 타임스케일 초기화
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Time.timeScale = 1;  // 재개 시 타임스케일 초기화
        Debug.Log("Quit pressed! Returning to Song Select Scene.");  // 임시 처리
        // UnityEngine.SceneManagement.SceneManager.LoadScene("SongSelectScene");  // 주석 처리
    }
}
