using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    /// <summary>
    /// 꼬치 막대
    /// </summary>
    WoodenSkewer woodenSkewer;

    private void Awake()
    {
        woodenSkewer = FindAnyObjectByType<WoodenSkewer>();
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
                woodenSkewer.isPassingThrough = true;
                Debug.Log("Hole1 통과 가능");
            }
        }
        else if (collision.CompareTag("Hole2"))
        {
            if (!woodenSkewer.isHole1 && woodenSkewer.isHole2 && !woodenSkewer.isHole3)
            {
                woodenSkewer.isPassingThrough = true;
                Debug.Log("Hole2 통과 가능");
            }
        }
        else if (collision.CompareTag("Hole3"))
        {
            if (!woodenSkewer.isHole1 && !woodenSkewer.isHole2 && woodenSkewer.isHole3)
            {
                woodenSkewer.isPassingThrough = true;
                Debug.Log("Hole3 통과 가능");
            }
        }
    }
}
