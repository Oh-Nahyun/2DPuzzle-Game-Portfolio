using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerText : MonoBehaviour
{
    /// <summary>
    /// 플레이어가 작성한 텍스트
    /// </summary>
    TextMeshProUGUI playerText;

    /// <summary>
    /// 정답 텍스트
    /// </summary>
    string correctText;

    private void Awake()
    {
        playerText = GetComponent<TextMeshProUGUI>();
        correctText = "맛있게 드세요!";
    }

    /// <summary>
    /// 텍스트가 같은지 확인하는 함수
    /// </summary>
    /// <returns>같으면 true, 다르면 false</returns>
    public bool IsSameText()
    {
        bool result = false;

        if (playerText == null)
        {
            result = false;
        }
        else
        {
            // 텍스트 비교
            if (playerText.text == correctText)
            {
                result = true; ////////////////////////////////////////////////////////////////////////////////
            }
        }

        return result;
    }
}
