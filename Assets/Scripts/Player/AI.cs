using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    /// <summary>
    /// [게임 난이도] AI의 플레이 속도
    /// </summary>
    public float playSpeed = 10.0f;

    /// <summary>
    /// AI의 게임 플레이 코루틴
    /// </summary>
    IEnumerator playGameAI()
    {
        yield return new WaitForSeconds(playSpeed);
        Debug.Log("<< AI Win >>");
        ////////////////////////////////////////////////////////////////////////////////
    }
}
