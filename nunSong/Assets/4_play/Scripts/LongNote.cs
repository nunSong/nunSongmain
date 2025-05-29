using UnityEngine;

public class LongNote : MonoBehaviour
{
    public KeyCode key;
    public float speed = 100f;
    public float holdDuration = 2f;  // 롱노트 유지 시간(초)
    private bool isHolding = false;
    private float holdTimer = 0f;

    private float comboIncreaseInterval = 1f;  // n초당 1콤보
    private float comboTimer = 0f;

    void Update()
    {
        // 노트 하강
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // 롱노트 시작 조건 (키 입력 및 판정 성공)
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
                Debug.Log($"Long Note Start! distance: {distance}, result: {result}");
            }
            else
            {
                Debug.Log($"Long Note Miss! distance: {distance}");
            }
        }

        // 롱노트 유지 중
        if (isHolding)
        {
            holdTimer -= Time.deltaTime;
            comboTimer += Time.deltaTime;

            if (comboTimer >= comboIncreaseInterval)
            {
                comboTimer = 0f;
                ComboManager.Instance.IncreaseCombo();
                FeverManager.Instance.AddFeverPoints(100);  // 피버 포인트 누적 (선택)
            }

            // 유지 시간 종료 시 성공 처리
            if (holdTimer <= 0)
            {
                isHolding = false;
                Destroy(gameObject);
                Debug.Log("Long Note Completed!");
            }

            // 키 입력 중단 시 실패 처리
            if (Input.GetKeyUp(key))
            {
                isHolding = false;
                LifeGauge.Instance.Reduce();
                ComboManager.Instance.ResetCombo();  // (선택) 실패 시 콤보 초기화
                Destroy(gameObject);
                Debug.Log("Long Note Missed!");
            }
        }

        // 화면 아래로 벗어날 경우 Miss 처리
        if (transform.position.y < -10f && !isHolding)
        {
            ComboManager.Instance.ResetCombo();
            LifeGauge.Instance.Reduce();
            Destroy(gameObject);
            Debug.Log("Long Note Out of Bounds - Miss!");
        }
    }
}
