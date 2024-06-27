using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 카드 매니저
    /// </summary>
    CardManager cardManager;

    private void Awake()
    {
        cardManager = FindAnyObjectByType<CardManager>();
    }

    private void Start()
    {
        // 카드 랜덤 뽑기
        cardManager.DrawRandomCard();
    }
}
