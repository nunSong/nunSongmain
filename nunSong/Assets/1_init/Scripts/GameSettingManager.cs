using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    }

    void Update()
    {

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
        // ShowSavePopup();
        Debug.Log("------설정이 초기화되었습니다.------");
        Debug.Log($"노트 속도: {noteSpeed}");
        Debug.Log($"판정 보정: {noteLatency} ms");
        Debug.Log("-------------------------------");
    }
}
