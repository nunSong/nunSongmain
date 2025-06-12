using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    public GameObject pausePopup;  // 일시정지 팝업 오브젝트
    private bool isPaused = false;
    public AudioSource bgmSource;  // 배경음악 AudioSource 연결


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

        // 오디오도 같이 Pause/UnPause 처리
        if (bgmSource != null)
        {
            if (isPaused)
                bgmSource.Pause();       // 음악 멈춤
            else
                bgmSource.UnPause();     // 음악 재개
        }
    }

    public void Resume()
    {
        isPaused = false;
        if (pausePopup != null)
        {
            pausePopup.SetActive(false);
        }
        Time.timeScale = 1;
        
        if (bgmSource != null)
        {
            bgmSource.UnPause(); // 재개 시 음악도 다시 재생
        }
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
