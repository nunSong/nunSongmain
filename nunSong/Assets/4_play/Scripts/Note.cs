using UnityEngine;

public class Note : MonoBehaviour
{
    public int laneNumber;  // 1~4
    public float speed = 100f;
    private bool judged = false;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        KeyCode key = LaneManager.Instance.GetKeyForLane(laneNumber);

        // 거리 계산
        float distance = Mathf.Abs(transform.position.y - JudgmentLine.Instance.transform.position.y);

        // 판정선 goodRange까지 거리 허용
        if (distance <= JudgmentLine.Instance.goodRange && Input.GetKeyDown(key) && !judged)
        {
            string result = JudgmentLine.Instance.Judge(distance);
            Debug.Log($"Key {key} pressed for {gameObject.name} with distance {distance}. Result: {result}");

            judged = true;
            ScoreManager.Instance.AddScore(result);

            if (result != "Miss") LifeGauge.Instance.Increase(result);
            else LifeGauge.Instance.Reduce();

            Destroy(gameObject);
        }

        if (transform.position.y < -10f && !judged)
        {
            judged = true;
            ScoreManager.Instance.AddScore("Miss");
            LifeGauge.Instance.Reduce();
            Destroy(gameObject);
        }
    }
}
