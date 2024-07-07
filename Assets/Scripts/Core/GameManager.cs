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
    /// 플레이어가 [FINISH] 버튼을 눌렀는지 아닌지 확인용 변수
    /// </summary>
    public bool isPlayerFinish = false;

    /// <summary>
    /// 게임 종료를 알리는 델리게이트
    /// (int : winner 표시용 변수 (1 : Player, 2 : AI))
    /// </summary>
    public Action<int> onGameFinish;

    /// <summary>
    /// 텍스트 패널 오브젝트
    /// </summary>
    public GameObject textPanel;

    /// <summary>
    /// 이동에 걸리는 시간 (초 단위)
    /// </summary>
    public float duration = 5.0f;

    /// <summary>
    /// 점수 패널 오브젝트
    /// </summary>
    public GameObject scorePanel;

    /// <summary>
    /// [NEXT] 버튼
    /// </summary>
    public GameObject nextButton;

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
        onGameFinish += (index) => GameFinish(index);
    }

    private void Start()
    {
        GameStart();
    }

    /// <summary>
    /// 게임 시작 처리 함수
    /// </summary>
    public void GameStart()
    {
        // 메인 카메라 위치 초기화
        mainCamera.transform.position = new Vector3(0, 0, -10.0f);

        // 꼬치 막대 및 재료 위치 초기화
        player.isStartAgain();

        // 게임 시작 코루틴 시작
        StopAllCoroutines();
        StartCoroutine(GameStartCoroutine());
    }

    /// <summary>
    /// 게임 시작 코루틴
    /// </summary>
    IEnumerator GameStartCoroutine()
    {
        // 점수 패널 비활성화
        scorePanel.SetActive(false);

        // [NEXT] 버튼 비활성화
        nextButton.SetActive(false);

        // [SHOUT] 버튼 비활성화
        shoutButton.gameObject.SetActive(false);

        // 카운트다운 시작
        countDown.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        countDown.SetActive(false);

        // Player 플레이 종료 변수 초기화
        isPlayerFinish = false;

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
    void GameFinish(int index)
    {
        // 텍스트 패널 비활성화
        textPanel.SetActive(false);

        // [SHOUT] 버튼 비활성화
        shoutButton.gameObject.SetActive(false);

        // 메인 카메라 이동
        StartCoroutine(MoveCamera(new Vector3(0, 15.0f, -10.0f), duration));

        // 승리 처리
        if (index == 1)
        {
            // Player가 이긴 경우
            Debug.Log("<< Winner : Player >>");
            player.isTheEnd();
            player.AddPlayerScore(cardManager.currentCard.cardScore);
        }
        else if (index == 2)
        {
            // AI가 이긴 경우
            Debug.Log("<< Winner : AI >>");
            ai.AddAIScore(cardManager.currentCard.cardScore);
        }
    }

    /// <summary>
    /// 메인 카메라 이동 처리 함수
    /// </summary>
    /// <param name="targetPosition">목표 위치</param>
    /// <param name="duration">이동에 걸리는 시간</param>
    IEnumerator MoveCamera(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = mainCamera.transform.position;  // 메인 카메라 초기 위치
        float elapsedTime = 0;                                  // 타이머

        while (elapsedTime < duration)
        {
            // 카메라 위치를 서서히 변경
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 마지막 위치를 정확히 맞춤
        mainCamera.transform.position = targetPosition;

        // 점수 패널 활성화
        scorePanel.SetActive(true);

        // [NEXT] 버튼 활성화
        nextButton.SetActive(true);
    }
}
