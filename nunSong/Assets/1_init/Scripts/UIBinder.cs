using UnityEngine;

public class UIBinder : MonoBehaviour
{
    public GameObject introUI;
    public GameObject mainUI;
    public GameObject settingUI;
    public GameObject selectUI;
    public GameObject songDetailUI;

    public GameObject noteSpeedTab;
    public GameObject latencyTab;
    public GameObject soundTab;
    public GameObject keyTab;
    public GameObject graphicsTab;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.BindUI(
                introUI,
                mainUI,
                settingUI,
                selectUI,
                songDetailUI,
                noteSpeedTab,
                latencyTab,
                soundTab,
                keyTab,
                graphicsTab
            );
        }
    }
}