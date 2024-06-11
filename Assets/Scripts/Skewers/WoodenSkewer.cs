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
}
