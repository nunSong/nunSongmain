using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{
    public static LifeGauge Instance;
    public Slider lifeSlider;
    public Image backgroundImage;
    public Image fillImage;

    private float life;
    private readonly float maxLife = 100f;
    private readonly float autoDecrease = 1f;
    private readonly float perfectRecovery = 3f;
    private readonly float greatRecovery = 1f;
    private readonly float missDamage = 5f;

    private void Awake() { Instance = this; }

    private void Start()
    {
        life = maxLife;
        lifeSlider.maxValue = maxLife;
        lifeSlider.value = life;

        // 색상 설정
        if (backgroundImage != null) backgroundImage.color = Color.white;          // 배경색: 하얀색
        if (fillImage != null) fillImage.color = Color.gray;                       // 채우기 색: 회색
    }

    private void Update()
    {
        if (!FeverManager.Instance.IsFeverActive())
        {
            life -= autoDecrease * Time.deltaTime;
            lifeSlider.value = life;
        }

        if (life <= 0) GameOver();
    }

    public void Increase(string grade)
    {
        if (grade == "Perfect") life += perfectRecovery;
        else if (grade == "Great") life += greatRecovery;
        if (life > maxLife) life = maxLife;
        lifeSlider.value = life;
    }

    public void Reduce()
    {
        if (!FeverManager.Instance.IsFeverActive())
        {
            life -= missDamage;
            lifeSlider.value = life;
        }
        if (life <= 0) GameOver();
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        MusicManager.Instance.StopMusic();  // 음악 정지 
    }
}
