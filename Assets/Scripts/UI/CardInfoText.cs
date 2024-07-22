using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardInfoText : MonoBehaviour
{
    /// <summary>
    /// 카드 정보
    /// </summary>
    public TextMeshProUGUI cardInfo;

    private void Awake()
    {
        cardInfo = GetComponent<TextMeshProUGUI>();
    }
}
