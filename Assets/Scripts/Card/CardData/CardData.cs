using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카드 한 종류의 정보를 저장하는 스크립터블 오브젝트
[CreateAssetMenu(fileName = "New Card Data", menuName = "Scriptable Object/Card Data", order = 0)]
public class CardData : ScriptableObject
{
    [Header("카드 기본 정보")]
    public string cardName = "카드";
    public string cardDescription = "설명";
    public Sprite cardIcon;
    public int cardScore = 0;
    public GameObject[] cardInfoArray;
}
