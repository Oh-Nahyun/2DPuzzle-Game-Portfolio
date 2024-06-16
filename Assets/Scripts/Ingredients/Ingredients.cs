using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : MonoBehaviour
{
    /// <summary>
    /// 꼬치 재료 [고기]의 원래 위치
    /// </summary>
    public Vector2 meatPosition;

    /// <summary>
    /// 꼬치 재료 [피망]의 원래 위치
    /// </summary>
    public Vector2 pepperPosition;

    /// <summary>
    /// 꼬치 재료 [새우]의 원래 위치
    /// </summary>
    public Vector2 shrimpPosition;

    /// <summary>
    /// 꼬치 재료 [토마토]의 원래 위치
    /// </summary>
    public Vector2 tomatoPosition;

    /// <summary>
    /// 꼬치 재료 [치즈]의 원래 위치
    /// </summary>
    public Vector2 cheesePosition;

    /// <summary>
    /// 꼬치 재료 [치즈]의 원래 각도
    /// </summary>
    public Quaternion cheeseRotation;

    /// <summary>
    /// 꼬치 재료 [치즈] 트랜스폼
    /// </summary>
    Transform cheeseObj;

    /// <summary>
    /// 꼬치 재료 [베이컨]의 원래 위치
    /// </summary>
    public Vector2 baconPosition;

    /// <summary>
    /// 꼬치 재료 [베이컨]의 원래 각도
    /// </summary>
    public Quaternion baconRotation;

    /// <summary>
    /// 꼬치 재료 [베이컨] 트랜스폼
    /// </summary>
    Transform baconObj;

    private void Awake()
    {
        meatPosition = transform.GetChild(0).position;
        pepperPosition = transform.GetChild(1).position;
        shrimpPosition = transform.GetChild(2).position;
        tomatoPosition = transform.GetChild(3).position;

        cheeseObj = transform.GetChild(4);
        cheesePosition = cheeseObj.GetChild(0).position;
        cheeseRotation = cheeseObj.GetChild(0).rotation;

        baconObj = transform.GetChild(5);
        baconPosition = baconObj.GetChild(0).position;
        baconRotation = baconObj.GetChild(0).rotation;
    }
}
