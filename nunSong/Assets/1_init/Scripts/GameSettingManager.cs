using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GameSettingManager : MonoBehaviour
{
    [Header("Note Speed")]
    [SerializeField] private TMP_Text noteSpeedDisplay;
    private float noteSpeed = 9.0f;

    private readonly float minNoteSpeed = 1.0f;
    private readonly float maxNoteSpeed = 10.0f;

    [Header("Note Latency")]
    [SerializeField] private TMP_Text noteLatencyDisplay;
    private int noteLatency = 0;
    private readonly int minLatency = -30;
    private readonly int maxLatency = 30;

    [Header("Sound Settings")]
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider soundEffectSlider;
    [SerializeField] private Slider correctVolumeSlider;

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource soundEffectSource;
    [SerializeField] private AudioSource correctSoundSource;

    [Header("Key Bindings")]
    [SerializeField] private TMP_Text qKeyText;
    [SerializeField] private TMP_Text wKeyText;
    [SerializeField] private TMP_Text eKeyText;
    [SerializeField] private TMP_Text rKeyText;
    private TMP_Text selectedKeyText = null; // 현재 키를 바꾸려는 텍스트
    private HashSet<KeyCode> assignedKeys = new HashSet<KeyCode>(); // 이미 할당된 키를 추적하기 위한 집합

    [Header("Display Settings")]
    [SerializeField] private Toggle backgroundToggle;
    [SerializeField] private TMP_Text screenModeText;
    [SerializeField] private TMP_Text graphicQualityText;

    private string[] screenModes = { "전체 화면", "창 모드" };
    private int currentScreenModeIndex = 1; // 기본 창 모드

    private string[] graphicQualities = { "낮음", "중간", "높음" };
    private int currentGraphicQualityIndex = 2; // 기본 높음

    void Start()
    {
        UpdateNoteSpeedDisplay();
        UpdateNoteLatencyDisplay();

        // 초기 슬라이더 값 설정
        bgmVolumeSlider.value = 0.5f;
        soundEffectSlider.value = 0.5f;
        correctVolumeSlider.value = 0.5f;

        // 초기 볼륨 적용
        UpdateSoundVolume(0.3f);
        UpdateSoundEffectVolume(0.3f);
        UpdateCorrectVolume(0.3f);

        //slider 이벤트 연결
        bgmVolumeSlider.onValueChanged.AddListener(UpdateSoundVolume);
        soundEffectSlider.onValueChanged.AddListener(UpdateSoundEffectVolume);
        correctVolumeSlider.onValueChanged.AddListener(UpdateCorrectVolume);

        // 키 초기 설정
        qKeyText.text = "Q";
        wKeyText.text = "W";
        eKeyText.text = "E";
        rKeyText.text = "R";

        RegisterInitialKeys(); // 초기 키 등록

        backgroundToggle.isOn = true; // 기본값: 켬
        backgroundToggle.onValueChanged.AddListener(OnBackgroundToggleChanged);

        screenModeText.text = screenModes[currentScreenModeIndex];
        graphicQualityText.text = graphicQualities[currentGraphicQualityIndex];
    }

    void Update()
    {
        HandleKeyInputs();

    }

    // Note Speed Control
    public void IncreaseNoteSpeed()
    {
        noteSpeed = Mathf.Min(noteSpeed + 0.5f, maxNoteSpeed);
        UpdateNoteSpeedDisplay();
    }

    public void DecreaseNoteSpeed()
    {
        noteSpeed = Mathf.Max(noteSpeed - 0.5f, minNoteSpeed);
        UpdateNoteSpeedDisplay();
    }

    private void UpdateNoteSpeedDisplay()
    {
        noteSpeedDisplay.text = noteSpeed.ToString("F1");
    }

    // Note Latency Control
    public void IncreaseLatency()
    {
        noteLatency = Mathf.Min(noteLatency + 1, maxLatency);
        UpdateNoteLatencyDisplay();
    }

    public void DecreaseLatency()
    {
        noteLatency = Mathf.Max(noteLatency - 1, minLatency);
        UpdateNoteLatencyDisplay();
    }

    private void UpdateNoteLatencyDisplay()
    {
        noteLatencyDisplay.text = $"{(noteLatency >= 0 ? "+" : "")}{noteLatency} ms";
    }

    // Sound Volume Control
        private void UpdateSoundVolume(float value)
    {
        if (bgmSource != null)
            bgmSource.volume = value;
    }

    private void UpdateSoundEffectVolume(float value)
    {
        if (soundEffectSource != null)
            soundEffectSource.volume = value;
    }

    private void UpdateCorrectVolume(float value)
    {
        if (correctSoundSource != null)
            correctSoundSource.volume = value;
    }

    // Key Bindings
    private void HandleKeyInputs()
    {
        if (selectedKeyText != null)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    // 알파벳 또는 방향키만 허용
                    if ((key >= KeyCode.A && key <= KeyCode.Z) ||
                        key == KeyCode.LeftArrow || key == KeyCode.RightArrow ||
                        key == KeyCode.UpArrow || key == KeyCode.DownArrow)
                    {
                        if (assignedKeys.Contains(key))
                        {
                            Debug.Log("이미 사용 중인 키입니다.");
                            return;
                        }

                        // 기존 키 제거
                        KeyCode oldKey;
                        if (TryGetAssignedKey(selectedKeyText, out oldKey))
                            assignedKeys.Remove(oldKey);

                        // 새 키 등록
                        selectedKeyText.text = KeyToDisplayName(key);
                        assignedKeys.Add(key);
                        selectedKeyText = null;
                        return;
                    }
                    else
                    {
                        Debug.Log("알파벳 또는 방향키만 사용할 수 있습니다.");
                        return;
                    }
                }
            }
        }
    }

    // 현재 텍스트에 저장된 키코드를 파싱
    private bool TryGetAssignedKey(TMP_Text text, out KeyCode key)
    {
        return System.Enum.TryParse(text.text, out key);
    }

    private string KeyToDisplayName(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.UpArrow: return "↑";
            case KeyCode.DownArrow: return "↓";
            case KeyCode.LeftArrow: return "←";
            case KeyCode.RightArrow: return "→";
            default: return key.ToString(); // A~Z 등은 원래 이름 그대로
        }
    }

    // 버튼에서 호출
    public void OnClickQ() => StartKeyRebind(qKeyText);
    public void OnClickW() => StartKeyRebind(wKeyText);
    public void OnClickE() => StartKeyRebind(eKeyText);
    public void OnClickR() => StartKeyRebind(rKeyText);

    private void StartKeyRebind(TMP_Text keyText)
    {
        selectedKeyText = keyText;
        Debug.Log($"{keyText.name} 키 입력 대기 중...");
    }

    private void RegisterInitialKeys()
    {
        TryAddKey(qKeyText.text);
        TryAddKey(wKeyText.text);
        TryAddKey(eKeyText.text);
        TryAddKey(rKeyText.text);
    }

    private void TryAddKey(string keyName)
    {
        if (System.Enum.TryParse(keyName, out KeyCode key))
        {
            assignedKeys.Add(key);
        }
    }

    public void onClickResetKeyBindings()
    {
        // 텍스트 초기화
        qKeyText.text = "Q";
        wKeyText.text = "W";
        eKeyText.text = "E";
        rKeyText.text = "R";
        assignedKeys.Clear();
        RegisterInitialKeys();
        Debug.Log("키 바인딩이 초기화되었습니다.");
    }

        // 플레이 배경 토글
    private void OnBackgroundToggleChanged(bool isOn)
    {
        Debug.Log("플레이 배경: " + (isOn ? "켬" : "끔"));
        // 여기서 배경 이미지 켜기/끄기 구현
    }

    // 화면 모드
    public void OnClickScreenModeLeft()
    {
        if (currentScreenModeIndex > 0)
        {
            currentScreenModeIndex--;
            UpdateScreenModeText();
        }
    }
    public void OnClickScreenModeRight()
    {
        if (currentScreenModeIndex < screenModes.Length - 1)
        {
            currentScreenModeIndex++;
            UpdateScreenModeText();
        }
    }
    private void UpdateScreenModeText()
    {
        screenModeText.text = screenModes[currentScreenModeIndex];
        Debug.Log("화면 모드 변경: " + screenModes[currentScreenModeIndex]);

        // 실제 적용
        if (currentScreenModeIndex == 0)
        {
            // 전체 화면
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.ExclusiveFullScreen);
        }
        else
        {
            // 창 모드 (기본 해상도 1280x720)
            Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
        }
    }

    // 그래픽 품질
    public void OnClickGraphicQualityLeft()
    {
        if (currentGraphicQualityIndex > 0)
        {
            currentGraphicQualityIndex--;
            UpdateGraphicQualityText();
        }
    }
    public void OnClickGraphicQualityRight()
    {
        if (currentGraphicQualityIndex < graphicQualities.Length - 1)
        {
            currentGraphicQualityIndex++;
            UpdateGraphicQualityText();
        }
    }
    private void UpdateGraphicQualityText()
    {
        graphicQualityText.text = graphicQualities[currentGraphicQualityIndex];
        Debug.Log("그래픽 품질: " + graphicQualities[currentGraphicQualityIndex]);

        // 선택된 품질에 따라 렌더링 해상도 비율 설정
        switch (currentGraphicQualityIndex)
        {
            case 0: // 낮음 (720p 수준)
                ScalableBufferManager.ResizeBuffers(0.67f, 0.67f); // 약 720p
                break;
            case 1: // 중간 (900p 수준)
                ScalableBufferManager.ResizeBuffers(0.83f, 0.83f); // 약 900p
                break;
            case 2: // 높음 (1080p 수준)
                ScalableBufferManager.ResizeBuffers(1.0f, 1.0f); // 1:1
                break;
        }
    }

    // Save & Reset
    public void ResetSettings()
    {
        noteSpeed = 9.0f;
        noteLatency = 0;
        UpdateNoteSpeedDisplay();
        UpdateNoteLatencyDisplay();
        bgmVolumeSlider.value = 0.5f;
        soundEffectSlider.value = 0.5f;
        correctVolumeSlider.value = 0.5f;

        // 키 초기화
        qKeyText.text = "Q";
        wKeyText.text = "W";
        eKeyText.text = "E";
        rKeyText.text = "R";

        // 키 재등록
        assignedKeys.Clear();
        RegisterInitialKeys();

        // ShowSavePopup();
        Debug.Log("------설정이 초기화되었습니다.------");
        Debug.Log($"노트 속도: {noteSpeed}");
        Debug.Log($"판정 보정: {noteLatency} ms");
        Debug.Log("-------------------------------");
    }
}
