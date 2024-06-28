using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 카운트다운 게임 오브젝트
    /// </summary>
    public GameObject countDown;

    /// <summary>
    /// [SHOUT] 버튼
    /// </summary>
    ShoutButton shoutButton;

    /// <summary>
    /// 카드 매니저
    /// </summary>
    CardManager cardManager;

    /// <summary>
    /// AI
    /// </summary>
    AI ai;

    /// <summary>
    /// 게임 종료를 알리는 델리게이트
    /// (int : winner 표시용 변수 (1 : Player, 2 : AI))
    /// </summary>
    public Action<int> OnGameFinish;

    /// <summary>
    /// 메인 카메라
    /// </summary>
    public Camera mainCamera;

    private void Awake()
    {
        shoutButton = FindAnyObjectByType<ShoutButton>();
        cardManager = FindAnyObjectByType<CardManager>();
        ai = FindAnyObjectByType<AI>();

        // 메인 카메라 찾기
        mainCamera = Camera.main;
    }

    private void Start()
    {
        StopAllCoroutines();
        StartCoroutine(GameStart());
    }

    /// <summary>
    /// 게임 종료 처리 함수
    /// </summary>
    void GameFinish()
    {

    }

    /// <summary>
    /// 게임 시작 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator GameStart()
    {
        // [SHOUT] 버튼 비활성화
        shoutButton.gameObject.SetActive(false);

        // 카운트다운 시작
        countDown.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        countDown.SetActive(false);

        // [SHOUT] 버튼 활성화
        shoutButton.gameObject.SetActive(true);

        // 카드 랜덤 뽑기
        cardManager.DrawRandomCard();

        // AI 플레이 시작
        ai.PlayGameAI();
    }
}