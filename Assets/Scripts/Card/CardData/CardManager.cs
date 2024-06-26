using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    /// <summary>
    /// 카드의 데이터 배열
    /// </summary>
    public CardData[] cardDatabase;

    /// <summary>
    /// 현재 카드
    /// </summary>
    CardData currentCard;

    /// <summary>
    /// 카드 오브젝트
    /// </summary>
    CardObject cardObject;

    /// <summary>
    /// 플레이어 컨트롤러
    /// </summary>
    PlayerController playerController;

    private void Awake()
    {
        cardObject = FindAnyObjectByType<CardObject>();
        playerController = FindAnyObjectByType<PlayerController>();
    }

    void Start()
    {
        DrawRandomCard(); ///////////////////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 랜덤으로 카드를 뽑는 함수
    /// </summary>
    public void DrawRandomCard()
    {
        // 카드의 데이터 배열이 빈 경우
        if (cardDatabase.Length == 0)
        {
            Debug.LogWarning("Card database is empty!");
            return;
        }

        // 랜덤으로 카드 뽑기
        int randomIndex = Random.Range(0, cardDatabase.Length); // 랜덤 인덱스
        currentCard = cardDatabase[randomIndex];                // 현재 카드 = 카드 배열에서 임의로 뽑은 카드
        cardObject.ChangeCardSprite(currentCard);               // 현재 카드의 스프라이트로 변경
        Debug.Log($"Drawn Card: {currentCard.cardName}, Score: {currentCard.cardScore}");
    }

    /// <summary>
    /// 카드와 플레이어가 만든 꼬치가 같은지 비교하는 함수
    /// </summary>
    /// <returns>같으면 true, 다르면 false</returns>
    public bool IsSameCard()
    {
        if (currentCard == null)
        {
            Debug.LogWarning("No card has been drawn yet!");
            return false;
        }

        return currentCard.cardInfoArray == playerController.finishedObjectArray;
    }
}
