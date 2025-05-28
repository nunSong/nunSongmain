using UnityEngine;

public class NoteMover : MonoBehaviour
{
    public float speed = 300f;
    RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        rect.anchoredPosition -= new Vector2(0, speed * Time.deltaTime);
    }
}