using UnityEngine;

public class FeverManager : MonoBehaviour
{
    public static FeverManager Instance;
    public GameObject feverText;
    private bool feverActive = false;
    private readonly float duration = 10f;
    private readonly int feverThreshold = 3000; //추후 50000으로 변경
    private float timer = 0f;
    private int feverPoints = 0;

    private void Awake() { Instance = this; }

    private void Update()
    {
        if (!feverActive && feverPoints >= feverThreshold) ActivateFever();
        if (feverActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0) EndFever();
        }
    }

    public void AddFeverPoints(int amount) { if (!feverActive) feverPoints += amount; }
    public bool IsFeverActive() => feverActive;
    public float GetFeverTimer() => timer;
    public float GetFeverDuration() => duration;
    public int GetFeverPoints() => feverPoints;
    public int GetFeverThreshold() => feverThreshold;

    private void ActivateFever()
    {
        feverActive = true;
        timer = duration;
        feverText.SetActive(true);
        Debug.Log("Fever Time Activated!");
    }

    private void EndFever()
    {
        feverActive = false;
        feverPoints = 0;
        feverText.SetActive(false);
        Debug.Log("Fever Time Ended!");
    }
}
