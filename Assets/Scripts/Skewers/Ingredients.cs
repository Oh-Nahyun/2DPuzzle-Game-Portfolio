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

    private void Awake()
    {
        meatPosition = transform.GetChild(0).position;
        pepperPosition = transform.GetChild(1).position;
        shrimpPosition = transform.GetChild(2).position;
        tomatoPosition = transform.GetChild(3).position;
    }
}
