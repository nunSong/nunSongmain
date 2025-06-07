using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        GAMERESULT,
        END
    }

    E_STATE gamestate; //게임 상태 변수
    public static GameManager Instance { get; private set; } //싱글톤 인스턴스

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //씬 전환 시에도 파괴되지 않도록 설정
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject); //이미 인스턴스가 존재하면 중복 생성 방지
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

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

    [Header("Album Cover")]
    public GameObject cover_g1;
    public GameObject cover_g2;
    public GameObject cover_g3;
    public GameObject cover_g4;
    private int currentGradeIndex = 0;
    private GameObject[] gradeCovers;

    [Header("Song Detail UI by Grade")]
    public GameObject detail_g1;
    public GameObject detail_g2;
    public GameObject detail_g3;
    public GameObject detail_g4;
    private GameObject[] detailUIs;

    [Header("Grade별 미리듣기 Audio")]
    public AudioClip[] previewClips; // Grade 1~4용 미리듣기
    private AudioSource previewSource;

    void Start()
    {
        string sceneState = PlayerPrefs.GetString("InitSceneState", "INTRO");

        switch (sceneState)
        {
            case "SELECTSONG":
                gamestate = E_STATE.SELECTSONG;
                break;
            case "SONGDETAIL":
                gamestate = E_STATE.SONGDETAIL;
                break;
            case "INTRO":
            default:
                gamestate = E_STATE.INTRO;
                break;
        }

        PlayerPrefs.DeleteKey("InitSceneState");

        // 별도 처리: SongDetailUI 활성화 여부
        if (PlayerPrefs.GetInt("ActivateSongDetailUI", 0) == 1)
        {
            PlayerPrefs.SetInt("ActivateSongDetailUI", 0);
            gamestate = E_STATE.SONGDETAIL;
        }
        Debug.Log($"[GameManager] InitSceneState = {sceneState}, gamestate = {gamestate}");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "InitScene")
        {
            Debug.Log("[GameManager] InitScene 로드 감지");

            // UI 다시 연결
            StartCoroutine(DelayedReconnect());

            // 상태 복원
            string sceneState = PlayerPrefs.GetString("InitSceneState", "INTRO");
            switch (sceneState)
            {
                case "SELECTSONG":
                    gamestate = E_STATE.SELECTSONG;
                    break;
                case "SONGDETAIL":
                    gamestate = E_STATE.SONGDETAIL;
                    break;
                case "INTRO":
                default:
                    gamestate = E_STATE.INTRO;
                    break;
            }

            PlayerPrefs.DeleteKey("InitSceneState");
            Debug.Log($"[GameManager] InitSceneState = {sceneState}, gamestate = {gamestate}");

            if (PlayerPrefs.GetInt("ActivateSongDetailUI", 0) == 1)
            {
                PlayerPrefs.SetInt("ActivateSongDetailUI", 0);
                gamestate = E_STATE.SONGDETAIL;
            }
        }
    }

    private IEnumerator DelayedReconnect()
    {
        yield return new WaitForSeconds(0.1f); // 씬 생성 딜레이 고려
        ReconnectUIReferences();
        Debug.Log("<color=green>GameManager UI 재연결 완료!</color>");
    }

    private void ReconnectUIReferences()
    {
        ui_gameIntro = GameObject.Find("IntroUI");
        ui_mainCover = GameObject.Find("mainUI");
        ui_setting = GameObject.Find("SettingUI");
        ui_songSelect = GameObject.Find("SelectUI");
        ui_songDetail = GameObject.Find("SongDetailUI");

        setting_ui_noteSpeed = GameObject.Find("noteSpeedTab");
        setting_ui_correction = GameObject.Find("latencyTab");
        setting_ui_sound = GameObject.Find("soundTab");
        setting_ui_key = GameObject.Find("keyTab");
        setting_ui_graphics = GameObject.Find("graphicsTab");
    }

    void Update()
    {
        GameState();
    }

    void GameState()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //q입력하면 즉시 종료
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

            case E_STATE.GAMERESULT:
                break;

            case E_STATE.END:
                GameEnd();
                break;
        }
        PlayerPrefs.DeleteKey("InitSceneState");
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

    private void StopPreviewAudio()
    {
        if (previewSource != null && previewSource.isPlaying)
            previewSource.Stop();
    }

    public void OnClickGamePlay()
    {
        StopPreviewAudio();
        Debug.Log("게임시작 버튼 누름");

        string sceneName = "";
        switch (currentGradeIndex)
        {
            case 0:
                sceneName = "playScene1";
                break;
            case 1:
                sceneName = "playScene2";
                break;
            case 2:
                sceneName = "playScene3";
                break;
            case 3:
                sceneName = "playScene4";
                break;
            default:
                Debug.LogError("유효하지 않은 Grade Index: " + currentGradeIndex);
                return;
        }

        Debug.Log("씬 로드: " + sceneName);
        SceneManager.LoadScene(sceneName);
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
        Debug.Log("판정 보정 설정 버튼 누름");
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
        StopAwakeBGM();
        ui_gameIntro.SetActive(false);
        ui_mainCover.SetActive(false);
        ui_songSelect.SetActive(true); //곡 선택 화면을 띄움
        ui_songDetail.SetActive(false);

        if (previewSource == null)
            previewSource = gameObject.AddComponent<AudioSource>();

        gradeCovers = new GameObject[] { cover_g1, cover_g2, cover_g3, cover_g4 };
        currentGradeIndex = 0;
        UpdateGradeCover();
        PlayPreviewForCurrentGrade();
    }

    public void OnClickPrevGrade()
    {
        currentGradeIndex = (currentGradeIndex - 1 + gradeCovers.Length) % gradeCovers.Length;
        UpdateGradeCover();
    }

    public void OnClickNextGrade()
    {
        currentGradeIndex = (currentGradeIndex + 1) % gradeCovers.Length;
        UpdateGradeCover();
    }

    private void UpdateGradeCover()
    {
        for (int i = 0; i < gradeCovers.Length; i++)
        {
            gradeCovers[i].SetActive(i == currentGradeIndex);
        }
        PlayPreviewForCurrentGrade();
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
        detailUIs = new GameObject[] { detail_g1, detail_g2, detail_g3, detail_g4 };

        for (int i = 0; i < detailUIs.Length; i++)
        {
            detailUIs[i].SetActive(i == currentGradeIndex);
        }
        PlayPreviewForCurrentGrade();
    }
        private void StopAwakeBGM()
    {
        var smBGM = GameObject.Find("SMbgm");
        if (smBGM != null)
        {
            var audio = smBGM.GetComponent<AudioSource>();
            if (audio != null && audio.isPlaying)
                audio.Stop();
        }
    }

    private void PlayPreviewForCurrentGrade()
    {
        if (previewClips.Length > currentGradeIndex && previewClips[currentGradeIndex] != null)
        {
            previewSource.clip = previewClips[currentGradeIndex];
            previewSource.Play();
        }
    }

    public void OnClickBacktoSongSelect()
    {
        Debug.Log("뒤로가기 버튼 누름");
        gamestate = E_STATE.SELECTSONG;
    }

    void GameEnd()
    {
        Debug.Log("게임종료");
        Application.Quit(); //게임 종료
    }

    public void onClickGameResult()
    {
        Debug.Log("게임 결과 화면으로 이동");
        SceneManager.LoadScene("ResultScene");
    }

    // 메인화면으로 이동 (InitScene)
    public void OnClickGoToMainScene()
    {
        Debug.Log("메인화면으로 이동");
        PlayerPrefs.SetString("InitSceneState", "INTRO");
        SceneManager.LoadScene("InitScene");
    }

    // 재시작 (PlayScene)
    public void OnClickRestartGame()
    {
        Debug.Log("게임 재시작");
        SceneManager.LoadScene("PlayScene");
    }

    // 곡선택 > InitScene 으로 이동 후 songDetailUI 활성화 요청
    public void OnClickSelectSongDetailFromMain()
    {
        Debug.Log("InitScene으로 이동 후 SongDetailUI 활성화 요청");
        PlayerPrefs.SetString("InitSceneState", "SELECTSONG");
        PlayerPrefs.SetInt("ActivateSongDetailUI", 1); // 표시 플래그 저장
        SceneManager.LoadScene("InitScene");
    }
    public void SetCurrentGradeIndex(int index)
    {
        currentGradeIndex = index;
    }

    public int GetCurrentGradeIndex() => currentGradeIndex;
    
    public void BindUI(
    GameObject intro,
    GameObject main,
    GameObject setting,
    GameObject select,
    GameObject detail,
    GameObject note,
    GameObject latency,
    GameObject sound,
    GameObject key,
    GameObject graphics
)
    {
        ui_gameIntro = intro;
        ui_mainCover = main;
        ui_setting = setting;
        ui_songSelect = select;
        ui_songDetail = detail;

        setting_ui_noteSpeed = note;
        setting_ui_correction = latency;
        setting_ui_sound = sound;
        setting_ui_key = key;
        setting_ui_graphics = graphics;

        Debug.Log("<color=green>GameManager: UI 재연결 완료!</color>");
    }
}
