using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    //public CardData[] cardDatabase;
    //private CardData currentCard;

    //void Start()
    //{
    //    DrawRandomCard();
    //}

    //// 랜덤한 카드를 뽑는 함수
    //public void DrawRandomCard()
    //{
    //    if (cardDatabase.Length == 0)
    //    {
    //        Debug.LogWarning("Card database is empty!");
    //        return;
    //    }

    //    int randomIndex = Random.Range(0, cardDatabase.Length);
    //    currentCard = cardDatabase[randomIndex];
    //    Debug.Log($"Drawn Card: {currentCard.cardName}, Description: {currentCard.description}, Attack: {currentCard.attackPower}, Defense: {currentCard.defensePower}");

    //    // 여기서 currentCard를 UI에 표시하는 로직을 추가할 수 있습니다.
    //    // DisplayCard(currentCard);
    //}

    //// 두 카드가 같은지 비교하는 함수
    //public bool IsSameCard(CardData card)
    //{
    //    if (currentCard == null)
    //    {
    //        Debug.LogWarning("No card has been drawn yet!");
    //        return false;
    //    }

    //    return currentCard.cardName == card.cardName;
    //}

    //// 예제: 다른 스크립트에서 이 함수를 호출할 수 있습니다.
    //public void CheckIfSameCard(CardData card)
    //{
    //    if (IsSameCard(card))
    //    {
    //        Debug.Log("The cards are the same!");
    //    }
    //    else
    //    {
    //        Debug.Log("The cards are different.");
    //    }
    //}

    //void DisplayCard(CardData cardData)
    //{
    //    // 여기서 카드 데이터를 기반으로 실제 카드를 UI에 표시하는 로직을 구현합니다.
    //    // 예를 들어, UI 텍스트나 이미지 컴포넌트를 사용하여 카드 정보를 화면에 출력
    //}
}
