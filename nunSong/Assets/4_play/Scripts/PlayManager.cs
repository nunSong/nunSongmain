using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    public enum E_STATE //게임 상태 관리
    {
        NONE = 0, //첫 숫자를 설정
        PLAY,
        PAUSE,
        END
    }

    E_STATE gamestate; //게임 상태 변수

    public GameObject ui_gamePlay; //게임 플레이 UI
    public GameObject ui_gamePause; //게임 일시정지 UI
    public GameObject ui_gameEnd; //게임 종료 UI

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    int score = 0;
    int combo = 0;

    // -------------------[추가]-----------------------
    public float judgementLineY = -400f; // 판정라인 위치
    public float noteSpeed = 300f; // 노트 속도 (픽셀/초)

    // 판정 시간(ms)
    public float perfectTime = 50f;
    public float greatTime = 80f;
    public float goodTime = 120f;
    // ------------------------------------------------

    void Start()
    {
        gamestate = E_STATE.PLAY;
        SetText();
    }

    void Update()
    {
        GameState();

        // -------------------[추가]-----------------------
        if (gamestate == E_STATE.PLAY)
        {
            CheckInput(); // 스페이스바 판정 입력 처리
        }
        // ------------------------------------------------
    }

    public void GetScore()
    {
        score += 100;

        // 1000점마다 combo 증가
        if (score % 1000 == 0)
        {
            combo += 1;
        }

        SetText();
    }

    public void SetText()
    {
        scoreText.text = score.ToString();
        comboText.text = $"{combo}/100"; // combo를 "n/100" 형식으로 표시
    }

    void GameState()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //q입력하면 즉시 종료
        {
            gamestate = E_STATE.END;
        }

        switch (gamestate)
        {
            case E_STATE.NONE:
                break;

            case E_STATE.PLAY:
                GamePlay();
                break;

            case E_STATE.PAUSE:
                GamePause();
                break;

            case E_STATE.END:
                GameEnd();
                break;
        }
    }

    void GamePlay()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) // 위 방향키를 누르면 점수 증가
        {
            GetScore();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) // ESC키를 누르면 일시정지
        {
            gamestate = E_STATE.PAUSE;
        }

        ui_gamePlay.SetActive(true);
        ui_gamePause.SetActive(false);
        ui_gameEnd.SetActive(false);
    }

    void GamePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC키를 누르면 일시정지 해제
        {
            gamestate = E_STATE.PLAY;
        }

        ui_gamePlay.SetActive(true);
        ui_gamePause.SetActive(true);
        ui_gameEnd.SetActive(false);
    }

    public void OnClickPauseButton()
    {
        gamestate = E_STATE.PAUSE;
    }
    public void OnClickResumeButton()
    {
        gamestate = E_STATE.PLAY;
    }

    public void OnClickRestartButton()
    {
        gamestate = E_STATE.PLAY;
        score = 0;
        combo = 0;
        SetText();
    }
    public void OnClickMainMenuButton()
    {
        // 메인 메뉴로 돌아가는 로직을 추가
        SceneManager.LoadScene("initScene");
        Debug.Log("메인 메뉴로 돌아갑니다.");
    }

    void GameEnd()
    {
        ui_gamePlay.SetActive(false);
        ui_gamePause.SetActive(false);
        ui_gameEnd.SetActive(true);
        // 게임 종료 시점에서 점수와 콤보를 초기화
        score = 0;
        combo = 0;
    }

    // -------------------[추가: 스페이스바 판정 처리]-----------------------
    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject[] notes = GameObject.FindGameObjectsWithTag("Note");
            RectTransform nearestNote = null;
            float nearestTime = float.MaxValue;

            foreach (var note in notes)
            {
                RectTransform rt = note.GetComponent<RectTransform>();
                float distance = Mathf.Abs(rt.anchoredPosition.y - judgementLineY);

                // 거리 ÷ 속도 → 시간(ms)
                float timeDiff = distance / noteSpeed * 1000f;

                if (timeDiff < nearestTime)
                {
                    nearestTime = timeDiff;
                    nearestNote = rt;
                }
            }

            if (nearestNote != null)
            {
                if (nearestTime <= perfectTime)
                {
                    Debug.Log("Perfect!");
                    score += 1000;
                    combo += 1;
                    Destroy(nearestNote.gameObject);
                }
                else if (nearestTime <= greatTime)
                {
                    Debug.Log("Great!");
                    score += 700;
                    combo += 1;
                    Destroy(nearestNote.gameObject);
                }
                else if (nearestTime <= goodTime)
                {
                    Debug.Log("Good!");
                    score += 300;
                    combo += 1;
                    Destroy(nearestNote.gameObject);
                }
                else
                {
                    Debug.Log("Miss!");
                    combo = 0;
                }
                SetText();
            }
            else
            {
                Debug.Log("노트 없음!");
            }
        }
    }
    // ------------------------------------------------
}
