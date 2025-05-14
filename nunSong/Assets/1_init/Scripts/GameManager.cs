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
        SETTING,
        SELECTSONG,
        SONGDETAIL,
        END
    }

    E_STATE gamestate; //게임 상태 변수

    [Header("UI")]
    public GameObject ui_gameIntro; //게임 로고 및 온보딩 화면
    public GameObject ui_mainCover;
    public GameObject ui_setting;
    public GameObject ui_songSelect;
    public GameObject ui_songDetail;

    //Setting 화면별
    [Header("Setting UI")]
    public GameObject setting_ui_noteSpeed;
    public GameObject setting_ui_correction;
    public GameObject setting_ui_sound;
    public GameObject setting_ui_key;
    public GameObject setting_ui_graphics;

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

            case E_STATE.SETTING:
                gamestate = E_STATE.NONE;
                Settings();
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
        ui_setting.SetActive(false);

        yield return new WaitForSeconds(2f); //2초 기다림

        gamestate = E_STATE.HOMEMAIN;
    }

    void GameHomeMain()
    {
        ui_gameIntro.SetActive(false);
        ui_mainCover.SetActive(true); //메인화면을 띄움
        ui_songSelect.SetActive(false);
        ui_songDetail.SetActive(false);
        ui_setting.SetActive(false);
    }

    public void OnClickGameStart()
    {
        Debug.Log("게임시작 버튼 누름");
        gamestate = E_STATE.SELECTSONG;
    }

    public void OnClickGameSetting()
    {
        Debug.Log("설정 버튼 누름");
        gamestate = E_STATE.SETTING;
    }

    void Settings()
    {
        //메인 화면 통제
        ui_gameIntro.SetActive(false);
        ui_mainCover.SetActive(false);
        ui_songSelect.SetActive(false);
        ui_songDetail.SetActive(false);
        ui_setting.SetActive(true); //설정 화면을 띄움

        //설정 화면 통제
        setting_ui_noteSpeed.SetActive(true); //기본 설정창
        setting_ui_correction.SetActive(false);
        setting_ui_sound.SetActive(false);
        setting_ui_key.SetActive(false);
        setting_ui_graphics.SetActive(false);
    }
    public void OnClickSettingNoteSpeed()
    {
        Debug.Log("노트 속도 설정 버튼 누름");
        setting_ui_noteSpeed.SetActive(true);
        setting_ui_correction.SetActive(false);
        setting_ui_sound.SetActive(false);
        setting_ui_key.SetActive(false);
        setting_ui_graphics.SetActive(false);
    }
    public void OnClickSettingCorrection()
    {
        Debug.Log("정정 설정 버튼 누름");
        setting_ui_noteSpeed.SetActive(false);
        setting_ui_correction.SetActive(true);
        setting_ui_sound.SetActive(false);
        setting_ui_key.SetActive(false);
        setting_ui_graphics.SetActive(false);
    }
    public void OnClickSettingSound()
    {
        Debug.Log("사운드 설정 버튼 누름");
        setting_ui_noteSpeed.SetActive(false);
        setting_ui_correction.SetActive(false);
        setting_ui_sound.SetActive(true);
        setting_ui_key.SetActive(false);
        setting_ui_graphics.SetActive(false);
    }
    public void OnClickSettingKey()
    {
        Debug.Log("키 설정 버튼 누름");
        setting_ui_noteSpeed.SetActive(false);
        setting_ui_correction.SetActive(false);
        setting_ui_sound.SetActive(false);
        setting_ui_key.SetActive(true);
        setting_ui_graphics.SetActive(false);
    }
    public void OnClickSettingGraphics()
    {
        Debug.Log("그래픽 설정 버튼 누름");
        setting_ui_noteSpeed.SetActive(false);
        setting_ui_correction.SetActive(false);
        setting_ui_sound.SetActive(false);
        setting_ui_key.SetActive(false);
        setting_ui_graphics.SetActive(true);
    }
    public void OnClickSettingBack()
    {
        Debug.Log("뒤로가기 버튼 누름");
        gamestate = E_STATE.HOMEMAIN;
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

    public void OnClickBacktoMain()
    {
        Debug.Log("뒤로가기 버튼 누름");
        gamestate = E_STATE.HOMEMAIN;
    }

    public void OnClickSelect()
    {
        Debug.Log("곡 상세 버튼 누름");
        gamestate = E_STATE.SONGDETAIL;
    }

    void SongDetail()
    {
        ui_gameIntro.SetActive(false);
        ui_mainCover.SetActive(false);
        ui_songSelect.SetActive(false);
        ui_songDetail.SetActive(true); //곡 상세 화면을 띄움
    }

        public void OnClickBacktoSongSelect()
    {
        Debug.Log("뒤로가기 버튼 누름");
        gamestate = E_STATE.SELECTSONG;
    }

    void GameEnd()
    {
        Debug.Log("게임종료");
        // Application.Quit(); //게임 종료
    }
}
