using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    /// <summary>
    /// [Next] 버튼
    /// </summary>
    Button nextButton;

    /// <summary>
    /// 게임 매니저
    /// </summary>
    public GameManager gameManager;

    private void Awake()
    {
        nextButton = GetComponent<Button>();

        // [Next] 버튼이 눌려지면 AddListener로 등록한 함수 실행
        nextButton.onClick.AddListener(() =>
        {
            gameManager.GameStart(); // 게임 재시작
        });
    }
}
