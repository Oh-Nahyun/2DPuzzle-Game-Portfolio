using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinnerText : MonoBehaviour
{
    /// <summary>
    /// 우승자를 받아올 텍스트
    /// </summary>
    public string winner;

    /// <summary>
    /// 우승자
    /// </summary>
    TextMeshProUGUI winnerText;

    private void Awake()
    {
        winnerText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        RefreshWinner(winner);
    }

    /// <summary>
    /// 우승자 갱신 함수
    /// </summary>
    /// <param name="newWinner">새로운 우승자</param>
    private void RefreshWinner(string newWinner)
    {
        winnerText.text = newWinner;
    }
}
