using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{
    public static LifeGauge Instance;
    public Slider lifeSlider;
    private float life = 100f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        lifeSlider.maxValue = 100f;
        lifeSlider.value = life;
    }

    private void Update()
    {
        if (!FeverManager.Instance.IsFeverActive())
        {
            life -= 1f * Time.deltaTime;
            lifeSlider.value = life;
        }

        if (life <= 0)
        {
            GameOver();
        }
    }

    public void Increase(string grade)
    {
        if (grade == "Perfect") life += 3f;
        else if (grade == "Great") life += 1f;
        // Good은 변화 없음
        if (life > 100f) life = 100f;
        lifeSlider.value = life;
    }

    public void Reduce()
    {
        if (!FeverManager.Instance.IsFeverActive())
        {
            life -= 5f;
            lifeSlider.value = life;
        }
        if (life <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
    }
}
