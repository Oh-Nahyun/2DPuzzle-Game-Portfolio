using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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
    public CardData currentCard;

    /// <summary>
    /// AI의 플레이 속도
    /// </summary>
    public float aiPlaySpeed = 5.0f;

    /// <summary>
    /// 카드 인덱스 리스트
    /// </summary>
    public List<int> cardIndex;

    /// <summary>
    /// 랜덤 인덱스
    /// </summary>
    int randomIndex;

    /// <summary>
    /// 카드 오브젝트
    /// </summary>
    CardObject cardObject;

    /// <summary>
    /// AI
    /// </summary>
    AI ai;

    /// <summary>
    /// 플레이어 컨트롤러
    /// </summary>
    PlayerController playerController;

    /// <summary>
    /// 게임 매니저
    /// </summary>
    public GameManager gameManager;

    private void Awake()
    {
        cardObject = FindAnyObjectByType<CardObject>();
        playerController = FindAnyObjectByType<PlayerController>();
        ai = FindAnyObjectByType<AI>();
    }

    private void Start()
    {
        // 리스트 초기화
        cardIndex = new List<int>();

        // 0부터 cardDatabase의 길이만큼 리스트에 추가
        for (int i = 0; i < cardDatabase.Length; i++)
        {
            cardIndex.Add(i);
        }

        // 랜덤 인덱스 뽑기
        randomIndex = Random.Range(0, cardDatabase.Length);
    }

    /// <summary>
    /// 랜덤으로 카드를 뽑는 함수
    /// </summary>
    public void DrawRandomCard()
    {
        // 카드 혹은 카드의 데이터 배열이 빈 경우
        if (cardDatabase == null || cardDatabase.Length == 0)
        {
            // Debug.LogWarning("Card database is empty!");
            return;
        }

        // 랜덤으로 카드 뽑기
        if (cardIndex.Contains(randomIndex))
        {
            // 해당 랜덤 인덱스를 가지고 있는 경우
            currentCard = cardDatabase[randomIndex];                        // 현재 카드 = 카드 배열에서 임의로 뽑은 카드
            cardObject.ChangeCardSprite(currentCard);                       // 현재 카드의 스프라이트로 변경
            ai.playSpeed = currentCard.cardInfoArray.Length * aiPlaySpeed;  // AI의 플레이 속도 설정
            cardIndex.Remove(randomIndex);                                  // 카드 인덱스 리스트에서 삭제

            // 카드 정보 출력 (확인용)
            Debug.Log($"Drawn Card: {currentCard.cardName}, \nDescription: {currentCard.cardDescription}, \nScore: {currentCard.cardScore}");

            // 리스트 출력 (확인용)
            //foreach (int number in cardIndex)
            //{
            //    Debug.Log(number);
            //}
        }
        else
        {
            // 해당 랜덤 인덱스를 가지고 있지 않은 경우
            if (cardIndex.Count == 0)
            {
                Debug.Log("----- The End -----");
                gameManager.isFinal = true;         // 게임 종료 표시
                gameManager.onGameEnd.Invoke();     // 게임 종료 알림
                return;
            }
            else
            {
                randomIndex = Random.Range(0, cardDatabase.Length);
                DrawRandomCard();
            }
        }
    }

    /// <summary>
    /// 카드와 플레이어가 만든 꼬치가 같은지 비교하는 함수
    /// </summary>
    /// <returns>같으면 true, 다르면 false</returns>
    public bool IsSameCard()
    {
        bool result = true;

        if (currentCard == null)
        {
            result = false;
        }
        else
        {
            int index = currentCard.cardInfoArray.Length - 1;
            if (playerController.finishedObjectArray[index] == null)
            {
                result = false;
            }
            else
            {
                // 카드와 플레이어의 꼬치의 배열이 같은지 확인
                for (int i = 0; i < currentCard.cardInfoArray.Length; i++)
                {
                    // Debug.Log($"Card: {currentCard.cardInfoArray[i].name}");
                    // Debug.Log($"Player: {playerController.finishedObjectArray[i].name}");
                    if (currentCard.cardInfoArray[i].name != playerController.finishedObjectArray[i].name)
                    {
                        result = false; // 하나라도 같은 위치에 다른 재료가 있으면 false
                    }
                }
            }
        }

        return result;
    }
}
