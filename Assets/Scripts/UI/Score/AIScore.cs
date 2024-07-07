using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AIScore : MonoBehaviour
{
    /// <summary>
    /// 목표로 하는 최종 점수
    /// </summary>
    int goalScore = 0;

    /// <summary>
    /// 현재 보여지는 점수
    /// </summary>
    float currentScore = 0.0f;

    /// <summary>
    /// 점수가 올라가는 속도
    /// </summary>
    public float scoreUpSpeed = 50.0f;

    /// <summary>
    /// 점수
    /// </summary>
    TextMeshProUGUI aiScore;

    /// <summary>
    /// AI
    /// </summary>
    AI ai;

    private void Awake()
    {
        aiScore = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        ai = FindAnyObjectByType<AI>();
        ai.onAIScoreChange += RefreshScore;

        // 변수 초기화
        goalScore = 0;
        currentScore = 0.0f;
        aiScore.text = "AI : 000";
    }

    private void Update()
    {
        if (currentScore < goalScore) // 점수가 올라가는 도중인 경우
        {
            // 최소 scoreUpSpeed 보장
            float speed = Mathf.Max((goalScore - currentScore) * 5.0f, scoreUpSpeed);

            currentScore += Time.deltaTime * speed;
            currentScore = Mathf.Min(currentScore, goalScore);

            // 소수점 출력 안하기
            aiScore.text = $"AI : {currentScore:f0}";
        }
    }

    private void RefreshScore(int newScore)
    {
        goalScore = newScore;
    }
}

