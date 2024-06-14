using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
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
        // 막대의 중간 부분에 도달했는지 확인
        if ((collision.CompareTag("Clickable")
            || (collision.CompareTag("Hole1") && !collision.CompareTag("Hole2") && !collision.CompareTag("Hole3"))
            || (!collision.CompareTag("Hole1") && collision.CompareTag("Hole2") && !collision.CompareTag("Hole3"))
            || (!collision.CompareTag("Hole1") && !collision.CompareTag("Hole2") && collision.CompareTag("Hole3")))
            && woodenSkewer.IsPassingThrough)
        {
            woodenSkewer.isFinished = true;
            Debug.Log("막대의 중간 부분 도착 >> 막대 통과 완료");
        }
    }
}
