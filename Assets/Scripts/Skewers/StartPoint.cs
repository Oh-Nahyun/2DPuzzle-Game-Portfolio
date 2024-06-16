using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    /// <summary>
    /// 플레이어
    /// </summary>
    PlayerController player;

    /// <summary>
    /// 꼬치 막대
    /// </summary>
    WoodenSkewer woodenSkewer;

    /// <summary>
    /// 꼬치 재료 [치즈]
    /// </summary>
    Cheese cheese;

    /// <summary>
    /// 꼬치 재료 [베이컨]
    /// </summary>
    Bacon bacon;

    /// <summary>
    /// 구멍 번호
    /// </summary>
    int index = 0;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
        woodenSkewer = FindAnyObjectByType<WoodenSkewer>();
        cheese = FindAnyObjectByType<Cheese>();
        bacon = FindAnyObjectByType<Bacon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 꼬치 막대의 시작 부분에 도달했는지 확인
        if (collision.CompareTag("Clickable"))
        {
            woodenSkewer.isPassingThrough = true;
            // Debug.Log("[Skewer's StartPoint Arrived] 꼬치 막대 통과 가능");
        }
        else if (collision.CompareTag("Hole1"))
        {
            if (woodenSkewer.isHole1 && !woodenSkewer.isHole2 && !woodenSkewer.isHole3)
            {
                index = 1;
                CheckLayer(collision.gameObject);
                woodenSkewer.isPassingThrough = true;
            }
        }
        else if (collision.CompareTag("Hole2"))
        {
            if (!woodenSkewer.isHole1 && woodenSkewer.isHole2 && !woodenSkewer.isHole3)
            {
                index = 2;
                CheckLayer(collision.gameObject);
                woodenSkewer.isPassingThrough = true;
            }
        }
        else if (collision.CompareTag("Hole3"))
        {
            if (!woodenSkewer.isHole1 && !woodenSkewer.isHole2 && woodenSkewer.isHole3)
            {
                index = 3;
                CheckLayer(collision.gameObject);
                woodenSkewer.isPassingThrough = true;
            }
        }
    }

    /// <summary>
    /// 오브젝트 레이어 비교 함수
    /// </summary>
    /// <param name="obj">확인할 오브젝트</param>
    void CheckLayer(GameObject obj)
    {
        if (obj.layer == 6)
        {
            cheese.CheeseState = Cheese.CheeseMode.OneHole;
            OffsetChange(cheese.Cheese_1.transform);
        }
        else if (obj.layer == 7)
        {
            bacon.BaconState = Bacon.BaconMode.OneHole;
            OffsetChange(bacon.Bacon_1.transform);
        }
    }

    /// <summary>
    /// offset 변경 함수
    /// </summary>
    /// <param name="obj">계산할 오브젝트의 트랜스폼</param>
    void OffsetChange(Transform obj)
    {
        Transform firstHole = obj.GetChild(index - 1);
        CircleCollider2D holeCollider = firstHole.GetComponent<CircleCollider2D>();
        player.offset = -holeCollider.offset;
        Debug.Log(player.offset);
    }
}
