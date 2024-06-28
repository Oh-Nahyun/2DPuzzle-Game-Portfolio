using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    /// <summary>
    /// 스프라이트 렌더러
    /// </summary>
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 카드의 스프라이트를 변경하는 함수
    /// </summary>
    /// <param name="data">카드 데이터</param>
    public void ChangeCardSprite(CardData data)
    {
        spriteRenderer.sprite = data.cardIcon;
    }
}
