using UnityEngine;

public class ChainNote : MonoBehaviour
{
    public KeyCode key = KeyCode.Space;
    public float speed = 5f;
    private bool judged = false;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (Input.GetKeyDown(key) && !judged)
        {
        
            float distance = Mathf.Abs(transform.position.y - JudgmentLine.Instance.transform.position.y);
            string result = JudgmentLine.Instance.Judge(distance);
            Debug.Log($"Key {key} pressed for {gameObject.name} with distance {distance}. Result: {result}");
            
            judged = true;
            ScoreManager.Instance.AddScore(result);

            if (result != "Miss")
            {
                LifeGauge.Instance.Increase(result);
            }
            else
            {
                LifeGauge.Instance.Reduce();
            }

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
