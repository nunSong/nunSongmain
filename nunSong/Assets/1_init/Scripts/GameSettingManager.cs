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

    void Start()
    {
        UpdateNoteSpeedDisplay();
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

    // Save & Reset
    public void ResetSettings()
    {
        noteSpeed = 9.0f;
        UpdateNoteSpeedDisplay();
        // ShowSavePopup();
        Debug.Log($"설정이 초기화되었습니다. Speed : {noteSpeed}");
    }
}
