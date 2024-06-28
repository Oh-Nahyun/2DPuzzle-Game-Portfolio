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

    private void Awake()
    {
        playerText = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// 텍스트가 같은지 확인하는 함수
    /// </summary>
    /// <returns>같으면 true, 다르면 false</returns>
    public bool IsSameText()
    {
        bool result = true;

        // 텍스트 비교
        if (playerText.text.GetHashCode() != 470909301)
        {
            result = false;
        }

        return result;
    }
}
