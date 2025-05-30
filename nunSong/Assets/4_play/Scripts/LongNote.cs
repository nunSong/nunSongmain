using UnityEngine;

public class LongNote : MonoBehaviour
{
    public int laneNumber;  // 1~4
    public float speed = 100f;
    public float holdDuration = 2f;
    private bool isHolding = false;
    private float holdTimer = 0f;
    private float comboIncreaseInterval = 1f;
    private float comboTimer = 0f;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        KeyCode key = LaneManager.Instance.GetKeyForLane(laneNumber);

        if (!isHolding && Input.GetKeyDown(key))
        {
            float distance = Mathf.Abs(transform.position.y - JudgmentLine.Instance.transform.position.y);
            string result = JudgmentLine.Instance.Judge(distance);
            if (result != "Miss")
            {
                isHolding = true;
                holdTimer = holdDuration;
                ScoreManager.Instance.AddScore(result);
                LifeGauge.Instance.Increase(result);
                Debug.Log("Long Note Start!");
            }
        }

        if (isHolding)
        {
            holdTimer -= Time.deltaTime;
            comboTimer += Time.deltaTime;

            if (comboTimer >= comboIncreaseInterval)
            {
                comboTimer = 0f;
                ComboManager.Instance.IncreaseCombo();
                FeverManager.Instance.AddFeverPoints(100);
            }

            if (holdTimer <= 0)
            {
                isHolding = false;
                Destroy(gameObject);
                Debug.Log("Long Note Completed!");
            }

            if (Input.GetKeyUp(key))
            {
                isHolding = false;
                LifeGauge.Instance.Reduce();
                ComboManager.Instance.ResetCombo();
                Destroy(gameObject);
                Debug.Log("Long Note Missed!");
            }
        }

        if (transform.position.y < -10f && !isHolding)
        {
            ComboManager.Instance.ResetCombo();
            LifeGauge.Instance.Reduce();
            Destroy(gameObject);
            Debug.Log("Long Note Out of Bounds - Miss!");
        }
    }
}
