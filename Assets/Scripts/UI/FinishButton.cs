using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FinishButton : MonoBehaviour
{
    /// <summary>
    /// [완성] 버튼
    /// </summary>
    Button finishButton;

    /// <summary>
    /// 카드 매니저
    /// </summary>
    CardManager cardManager;

    private void Awake()
    {
        finishButton = GetComponent<Button>();
        cardManager = FindAnyObjectByType<CardManager>();

        // finish 버튼이 눌려지면 AddListener로 등록한 함수 실행
        finishButton.onClick.AddListener(() =>
        {
            if (cardManager.IsSameCard())
            {
                ///////////////////////////////////////////////////////////////////////////////
            }
        });
    }
}
