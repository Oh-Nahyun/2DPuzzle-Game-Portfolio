using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    /// <summary>
    /// 목표로 하는 최종 점수
    /// </summary>
    int goalScore = 0;

    /// <summary>
    /// 현재 보여지는 점수
    /// </summary>
    public float currentScore = 0.0f;

    /// <summary>
    /// 점수가 올라가는 속도
    /// </summary>
    public float scoreUpSpeed = 50.0f;

    /// <summary>
    /// 점수
    /// </summary>
    TextMeshProUGUI playerScore;

    /// <summary>
    /// Player
    /// </summary>
    PlayerController player;

    private void Awake()
    {
        playerScore = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        player.onPlayerScoreChange += RefreshScore;

        // 변수 초기화
        goalScore = 0;
        currentScore = 0.0f;
        playerScore.text = "Player : 000";
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
            playerScore.text = $"Player : {currentScore:f0}";
        }
    }

    /// <summary>
    /// 점수 갱신 함수
    /// </summary>
    /// <param name="newScore">새로운 점수</param>
    private void RefreshScore(int newScore)
    {
        goalScore = newScore;
    }
}

