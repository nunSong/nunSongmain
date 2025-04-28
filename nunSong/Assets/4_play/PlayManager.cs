using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class PlayManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    int score = 0;
    void Start()
    {
        SetText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //스페이스바를 누르면 점수 증가
        {
            GetScore();
        }
    }

    public void GetScore()
    {
        score += 100;
        SetText();
    }

    public void SetText()
    {
        scoreText.text = score.ToString();
    }
}
