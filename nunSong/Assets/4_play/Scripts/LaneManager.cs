using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public static LaneManager Instance;
    public KeyCode[] laneKeys = new KeyCode[4];  // 레인 1~4 키

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeLaneKeys();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeLaneKeys()
    {
        // Q/W/E/R 고정
        laneKeys[0] = KeyCode.Q;
        laneKeys[1] = KeyCode.W;
        laneKeys[2] = KeyCode.E;
        laneKeys[3] = KeyCode.R;
    }

    public KeyCode GetKeyForLane(int laneNumber)
    {
        return laneKeys[laneNumber - 1];  // laneNumber 1~4
    }
}
