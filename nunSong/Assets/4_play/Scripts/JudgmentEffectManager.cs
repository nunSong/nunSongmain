using UnityEngine;
using TMPro;

public class JudgmentEffectManager : MonoBehaviour
{
    public static JudgmentEffectManager Instance;

    public TextMeshProUGUI judgmentText;
    public float displayDuration = 1f;

    private void Awake()
    {
        Instance = this;
        judgmentText.gameObject.SetActive(false);
    }

    public void ShowJudgment(string result)
    {
        StopAllCoroutines();
        StartCoroutine(ShowJudgmentCoroutine(result));
    }

    private System.Collections.IEnumerator ShowJudgmentCoroutine(string result)
    {
        judgmentText.text = result;

        switch (result)
        {
            case "Perfect": judgmentText.color = Color.yellow; break;
            case "Great": judgmentText.color = Color.green; break;
            case "Good": judgmentText.color = Color.cyan; break;
            case "Miss": judgmentText.color = Color.red; break;
        }

        judgmentText.gameObject.SetActive(true);

        float elapsed = 0f;
        Vector3 originalScale = Vector3.one;
        Vector3 targetScale = Vector3.one * 1.5f;

        while (elapsed < displayDuration)
        {
            elapsed += Time.deltaTime;
            judgmentText.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / displayDuration);
            yield return null;
        }

        judgmentText.gameObject.SetActive(false);
        judgmentText.transform.localScale = originalScale;
    }
}
