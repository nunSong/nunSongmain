using UnityEngine;
using UnityEngine.UI;

public class UIButtonSound : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => SoundManager.Instance.PlayUIClick());
    }
}
