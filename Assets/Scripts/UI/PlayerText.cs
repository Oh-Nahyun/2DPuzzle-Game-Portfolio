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
    TextMeshProUGUI text;

    /// <summary>
    /// 플레이어가 작성한 텍스트의 해시 코드 (텍스트 비교 오류 해결을 위해 사용)
    /// </summary>
    const int playerHashCode = 470909301;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// 텍스트가 같은지 확인하는 함수
    /// </summary>
    /// <returns>같으면 true, 다르면 false</returns>
    public bool IsSameText()
    {
        bool result = true;

        // 텍스트 비교
        if (text.text.GetHashCode() != playerHashCode)
        {
            result = false;
        }

        return result;
    }

    /// <summary>
    ///// 플레이어가 전에 작성한 텍스트 삭제용 함수 (텍스트 오류로 인해 초기화 불가능 상태)
    /// </summary>
    public void GetEmptyText()
    {
        text.text = string.Empty;
        //Debug.Log($"PlayerText : {text.text}\nEmptyText : {string.Empty.GetHashCode()}");
    }
}
