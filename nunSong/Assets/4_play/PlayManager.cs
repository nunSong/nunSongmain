using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class PlayManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    int score = 0;
    int combo = 0;

    void Start()
    {
        SetText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바를 누르면 점수 증가
        {
            GetScore();
        }
    }

    public void GetScore()
    {
        score += 100;

        // 1000점마다 combo 증가
        if (score % 1000 == 0)
        {
            combo += 1;
        }

        SetText();
    }

    public void SetText()
    {
        scoreText.text = score.ToString();
        comboText.text = $"{combo}/100"; // combo를 "n/100" 형식으로 표시
    }
}
