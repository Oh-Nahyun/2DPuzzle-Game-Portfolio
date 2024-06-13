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
    /// 꼬치 재료 [치즈] 오브젝트
    /// </summary>
    public GameObject cheeseObj;

    /// <summary>
    /// 꼬치 재료 [베이컨]의 원래 위치
    /// </summary>
    public Vector2 baconPosition;

    /// <summary>
    /// 꼬치 재료 [베이컨]의 원래 각도
    /// </summary>
    public Quaternion baconRotation;

    /// <summary>
    /// 꼬치 재료 [베이컨] 오브젝트
    /// </summary>
    public GameObject baconObj;

    private void Awake()
    {
        meatPosition = transform.GetChild(0).position;
        pepperPosition = transform.GetChild(1).position;
        shrimpPosition = transform.GetChild(2).position;
        tomatoPosition = transform.GetChild(3).position;

        cheesePosition = transform.GetChild(4).position;
        cheeseRotation = transform.GetChild(4).rotation;
        cheeseObj = transform.GetChild(4).gameObject;

        baconPosition = transform.GetChild(5).position;
        baconRotation = transform.GetChild(5).rotation;
        baconObj = transform.GetChild(5).gameObject;
    }
}
