using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum E_STATE //게임 상태 관리
    {
        NONE = 0, //첫 숫자를 설정
        INTRO,
        HOMEMAIN,
        SELECTSONG,
        SONGDETAIL,
        END
    }

    E_STATE gamestate; //게임 상태 변수

    public GameObject ui_gameIntro; //게임 로고 및 온보딩 화면
    public GameObject ui_mainCover;
    public GameObject ui_songSelect;
    public GameObject ui_songDetail;

    void Start()
    {
        gamestate = E_STATE.INTRO;
    }

    void Update()
    {
        GameState();
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
            
            case E_STATE.INTRO:
                gamestate = E_STATE.NONE;
                StartCoroutine(GameIntro());
                break;
            
            case E_STATE.HOMEMAIN:
                gamestate = E_STATE.NONE;
                GameHomeMain();
                break;
            
            case E_STATE.SELECTSONG:
                gamestate = E_STATE.NONE;
                SongSelect();
                break;
            
            case E_STATE.SONGDETAIL:
                gamestate = E_STATE.NONE;
                SongDetail();
                break;
            
            case E_STATE.END:
                GameEnd();
                break;
        }
    }

    IEnumerator GameIntro()
    {
        ui_gameIntro.SetActive(true); //게임 로고를 띄움
        ui_mainCover.SetActive(false);
        ui_songSelect.SetActive(false);
        ui_songDetail.SetActive(false);

        yield return new WaitForSeconds(2f); //2초 기다림

        gamestate = E_STATE.HOMEMAIN;
    }

    void GameHomeMain()
    {
        ui_gameIntro.SetActive(false);
        ui_mainCover.SetActive(true); //메인화면을 띄움
        ui_songSelect.SetActive(false);
        ui_songDetail.SetActive(false);
    }

    public void OnClickGameStart()
    {
        Debug.Log("게임시작 버튼 누름");
        gamestate = E_STATE.SELECTSONG;
    }

    public void OnClickGameSetting()
    {
        Debug.Log("설정 버튼 누름");
    }

    public void OnClickGameRecord()
    {
        Debug.Log("Record 버튼 누름");
    }

    public void OnClickGameExit()
    {
        Debug.Log("게임 종료 버튼 누름");
        gamestate = E_STATE.END;
    }

    void SongSelect()
    {
        ui_gameIntro.SetActive(false);
        ui_mainCover.SetActive(false);
        ui_songSelect.SetActive(true); //곡 선택 화면을 띄움
        ui_songDetail.SetActive(false);
    }

    void SongDetail()
    {
        ui_gameIntro.SetActive(false);
        ui_mainCover.SetActive(false);
        ui_songSelect.SetActive(false);
        ui_songDetail.SetActive(true); //곡 상세 화면을 띄움
    }

    void GameEnd()
    {
        Debug.Log("게임종료");
        // Application.Quit(); //게임 종료
    }
}
