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
    /// 게임 종료를 알리는 델리게이트
    /// (int : winner 표시용 변수 (1 : Player, 2 : AI))
    /// </summary>
    public Action<int> onGameFinish;

    /// <summary>
    /// AI
    /// </summary>
    AI ai;

    /// <summary>
    /// Player
    /// </summary>
    PlayerController player;

    /// <summary>
    /// 메인 카메라
    /// </summary>
    public Camera mainCamera;

    private void Awake()
    {
        shoutButton = FindAnyObjectByType<ShoutButton>();
        cardManager = FindAnyObjectByType<CardManager>();

        ai = FindAnyObjectByType<AI>();
        player = FindAnyObjectByType<PlayerController>();

        // 메인 카메라 찾기
        mainCamera = Camera.main;

        // 델리게이트 연결하기
        onGameFinish = (index) => GameFinish(index); ////////////////////////////////////////////////////////////////////////////////
    }

    private void Start()
    {
        StopAllCoroutines();
        StartCoroutine(GameStart());
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

    /// <summary>
    /// 게임 종료 처리 함수 (1 : Player, 2 : AI))
    /// </summary>
    void GameFinish(int index) ////////////////////////////////////////////////////////////////////////////////
    {
        if (index == 1)
        {
            // Player가 이긴 경우
            player.isTheEnd();
        }
        else if (index == 2)
        {
            // AI가 이긴 경우
        }
    }
}
