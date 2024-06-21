using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenSkewer : MonoBehaviour
{
    /// <summary>
    /// 막대 통과 가능 여부를 나타내는 변수
    /// (막대의 시작 부분에 도달하면 true, 아니면 통과 불가이므로 false)
    /// </summary>
    public bool isPassingThrough = false;

    /// <summary>
    /// isPassingThrough 확인용 프로퍼티
    /// </summary>
    public bool IsPassingThrough => isPassingThrough;

    /// <summary>
    /// 막대 통과 완료 여부를 나타내는 변수
    /// </summary>
    public bool isFinished = false;

    /// <summary>
    /// isFinished 확인용 프로퍼티
    /// </summary>
    public bool IsFinished => isFinished;

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
    public int index = 0;

    /// <summary>
    /// 선택된 구멍이 무엇인지 확인하는 변수
    /// </summary>
    public bool isHole1 = false;
    public bool isHole2 = false;
    public bool isHole3 = false;

    /// <summary>
    /// Shift 선택이 가능한 상태인지에 대한 [치즈] 변수
    /// </summary>
    public bool canChooseCheeseHole1 = true;
    public bool canChooseCheeseHole2 = true;
    public bool canChooseCheeseHole3 = true;

    /// <summary>
    /// Shift 선택이 가능한 상태인지에 대한 [베이컨] 변수
    /// </summary>
    public bool canChooseBaconHole1 = true;
    public bool canChooseBaconHole2 = true;
    public bool canChooseBaconHole3 = true;

    /// <summary>
    /// 플레이어
    /// </summary>
    PlayerController player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
        cheese = FindAnyObjectByType<Cheese>();
        bacon = FindAnyObjectByType<Bacon>();
    }

    /// <summary>
    /// 꼬치 통과를 위한 오브젝트 레이어 비교 함수
    /// </summary>
    /// <param name="obj">확인할 오브젝트</param>
    public void CheckLayerForStart(GameObject obj)
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
        // Debug.Log(player.offset);
    }
}
