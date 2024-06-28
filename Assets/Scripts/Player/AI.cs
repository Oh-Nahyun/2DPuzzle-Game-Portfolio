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
    /// 게임 매니저
    /// </summary>
    GameManager gameManager; ////////////////////////////////////////////////////////////////////////////////

    private void Awake()
    {
        ////////////////////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// AI의 게임 플레이 함수
    /// </summary>
    public void PlayGameAI()
    {
        StartCoroutine(playGameAI());
    }

    /// <summary>
    /// AI의 게임 플레이 코루틴
    /// </summary>
    IEnumerator playGameAI()
    {
        yield return new WaitForSeconds(playSpeed);
        Debug.Log("<< Winner : AI >>");
        ////////////////////////////////////////////////////////////////////////////////
    }
}
