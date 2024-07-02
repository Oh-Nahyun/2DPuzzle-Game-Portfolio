using System;
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
    /// AI의 점수
    /// </summary>
    int aiScore = 0;

    /// <summary>
    /// AI의 점수 확인 및 설정용 프로퍼티
    /// </summary>
    public int AIScore
    {
        get => aiScore; // 읽기는 public
        private set     // 쓰기는 private
        {
            if (aiScore != value)
            {
                aiScore = Math.Min(value, 999);     // 최대 점수 999
                onAIScoreChange?.Invoke(aiScore);   // 이 델리게이트에 함수를 등록한 모든 대상에게 변경된 점수를 알림
            }
        }
    }

    /// <summary>
    /// AI의 점수가 변경되었음을 알리는 델리게이트
    /// (int : 변경된 점수)
    /// </summary>
    public Action<int> onAIScoreChange;

    /// <summary>
    /// 게임 매니저
    /// </summary>
    public GameManager gameManager;

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
        if (!gameManager.isPlayerFinish)
        {
            gameManager.onGameFinish?.Invoke(2);
        }
    }

    /// <summary>
    /// AI의 점수를 추가해주는 함수
    /// </summary>
    /// <param name="getScore">새로 얻은 점수</param>
    public void AddAIScore(int getScore)
    {
        AIScore += getScore;
    }
}
