using UnityEngine;
using TMPro;

public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance;
    public TextMeshProUGUI comboText;  // Text -> TMP
    public TextMeshProUGUI maxComboText;  // Text -> TMP

    private int combo = 0;
    private int maxCombo = 0;

    public int CurrentCombo => combo;

    private void Awake()
    {
        Instance = this;
    }

    public void IncreaseCombo()
    {
        combo++;
        comboText.text = combo.ToString();
        if (combo > maxCombo)
        {
            maxCombo = combo;
            maxComboText.text = maxCombo.ToString();
        }
    }

    public void ResetCombo()
    {
        combo = 0;
        comboText.text = "0";
    }
}
