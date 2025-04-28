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

    void Start()
    {
        gamestate = E_STATE.PLAY;
        SetText();
    }

    void Update()
    {
        GameState();
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
}
