using UnityEngine;
using UnityEngine.UI;

public class FeverManager : MonoBehaviour
{
    public static FeverManager Instance;
    public GameObject feverText;
    public Image feverGauge;

    private bool feverActive = false;
    private float duration = 10f;
    private float timer = 0f;
    private int feverPoints = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!feverActive && feverPoints >= 5000) //추후 50000으로 수정
        {
            ActivateFever();
        }

        if (feverActive)
        {
            timer -= Time.deltaTime;
            feverGauge.fillAmount = timer / duration;

            if (timer <= 0)
            {
                feverActive = false;
                feverPoints = 0;
                feverText.SetActive(false);
                feverGauge.fillAmount = 0;
            }
        }
    }

    public void AddFeverPoints(int amount)
    {
        if (!feverActive)
        {
            feverPoints += amount;
        }
    }

    public bool IsFeverActive() => feverActive;

    private void ActivateFever()
    {
        feverActive = true;
        timer = duration;
        feverText.SetActive(true);
        Debug.Log("Fever Time Activated!");
    }
}
