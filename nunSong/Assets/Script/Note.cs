using UnityEngine;

public class Note : MonoBehaviour
{
    public int laneNumber;
    public float speed = 100f;
    private bool judged = false;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        KeyCode key = LaneManager.Instance.GetKeyForLane(laneNumber);
        float distance = Mathf.Abs(transform.position.y - JudgmentLine.Instance.transform.position.y);

        if (distance <= JudgmentLine.Instance.goodRange && Input.GetKeyDown(key) && !judged)
        {
            judged = true;
            string result = JudgmentLine.Instance.Judge(distance);
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
