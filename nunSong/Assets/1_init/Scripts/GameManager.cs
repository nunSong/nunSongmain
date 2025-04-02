using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum E_STATE //게임 상태 관리 변수
    {
        NONE = 0, //첫 숫자를 설정
        INTRO,
        SELECTSONG,
        SONGDETAIL,
        END
    }

    public GameObject ui_mainCover;
    public GameObject ui_songSelect;
    public GameObject ui_songDetail;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClickStartButton()
    {
        Debug.Log("Start Button Clicked");
    }
}
