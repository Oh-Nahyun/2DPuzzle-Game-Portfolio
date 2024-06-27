using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    /// <summary>
    /// 카드 힌트 이미지
    /// </summary>
    Image image = null;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    /// <summary>
    /// UI의 스프라이트를 변경하는 함수
    /// </summary>
    /// <param name="data">카드 데이터</param>
    public void ChangeHintSprite(CardData data)
    {
        image.sprite = data.cardIcon;
    }
}
