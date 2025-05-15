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

    void Start()
    {
        UpdateNoteSpeedDisplay();
        UpdateNoteLatencyDisplay();
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

    // Save & Reset
    public void ResetSettings()
    {
        noteSpeed = 9.0f;
        noteLatency = 0;
        UpdateNoteSpeedDisplay();
        UpdateNoteLatencyDisplay();
        // ShowSavePopup();
        Debug.Log("------설정이 초기화되었습니다.------");
        Debug.Log($"노트 속도: {noteSpeed}");
        Debug.Log($"판정 보정: {noteLatency} ms");
        Debug.Log("-------------------------------");
    }
}
