using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 드래그 중인지 확인하는 변수
    /// </summary>
    bool isDragging = false;

    /// <summary>
    /// 오브젝트의 원래 위치
    /// </summary>
    Vector2 originalPosition;

    /// <summary>
    /// 오브젝트의 실시간 월드 좌표
    /// </summary>
    Vector2 worldPosition;

    /// <summary>
    /// 드래그 중인 오브젝트
    /// </summary>
    GameObject draggedObject;

    /// <summary>
    /// 배치 완료된 오브젝트 배열
    /// </summary>
    GameObject[] finishedObjectArray;

    /// <summary>
    /// 막대
    /// </summary>
    WoodenSkewer woodenSkewer;

    /// <summary>
    /// 메인 카메라
    /// </summary>
    public Camera mainCamera;

    /// <summary>
    /// 플레이어 인풋
    /// </summary>
    PlayerInputActions playerInputAction;

    private void Awake()
    {
        // 플레이어 인풋 액션
        playerInputAction = new PlayerInputActions();

        // 메인 카메라 찾기
        if (mainCamera == null)
        {
            mainCamera = Camera.main;                   // 카메라 설정
        }

        // 막대 찾기
        if (woodenSkewer == null)
        {
            woodenSkewer = FindAnyObjectByType<WoodenSkewer>();
        }

        // 오브젝트 배열 초기화
        if (finishedObjectArray == null || finishedObjectArray.Length == 0)
        {
            finishedObjectArray = new GameObject[10];   // 크기 설정
        }
    }

    private void OnEnable()
    {
        playerInputAction.Player.Enable();

        playerInputAction.Player.Click.performed += OnLeftClickInput;
        playerInputAction.Player.Click.canceled += OnLeftClickInput;

        playerInputAction.Player.Cancel.performed += OnRightClickInput;
        playerInputAction.Player.Cancel.canceled += OnRightClickInput;
    }

    private void OnDisable()
    {
        playerInputAction.Player.Cancel.canceled -= OnRightClickInput;
        playerInputAction.Player.Cancel.performed -= OnRightClickInput;

        playerInputAction.Player.Click.canceled -= OnLeftClickInput;
        playerInputAction.Player.Click.performed -= OnLeftClickInput;

        playerInputAction.Player.Disable();
    }

    private void Update()
    {
        OnDrag();
    }

    /// <summary>
    /// 마우스 왼쪽 버튼 클릭 처리 함수
    /// </summary>
    private void OnLeftClickInput(InputAction.CallbackContext context)
    {
        if (!context.canceled)
        {
            // 마우스 왼쪽 버튼을 누른 경우
            OnSelect();
        }
        else
        {
            // 마우스 왼쪽 버튼을 뗀 경우
            OnRelease();
        }
    }

    /// <summary>
    /// 마우스 오른쪽 버튼 클릭 처리 함수
    /// </summary>
    private void OnRightClickInput(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            // 마우스 오른쪽 버튼을 눌렀다 뗀 경우
            OnCancel();
        }
    }

    /// <summary>
    /// 선택 처리 함수
    /// </summary>
    void OnSelect()
    {
        // 마우스 클릭된 위치
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        // 레이 이용
        Ray ray = mainCamera.ScreenPointToRay(mousePosition); // 레이 생성
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray); // 클릭된 오브젝트를 감지

        // 충돌 확인
        if (hit.collider != null)
        {
            // 선택한 오브젝트 저장
            GameObject clickedObject = hit.collider.gameObject;

            // 클릭된 오브젝트가 "Clickable" 태그를 가지고 있는지 확인
            if (clickedObject.CompareTag("Clickable"))
            {
                draggedObject = clickedObject;                          // 드래그 한 오브젝트 설정
                isDragging = true;                                      // 드래그 상태 활성화
                originalPosition = draggedObject.transform.position;    // 원래 위치 저장
            }

            // 각 구멍에 따른 행동 처리
            if (hit.collider.CompareTag("Hole1"))
            {
                FirstHoleAction();
            }
            else if (hit.collider.CompareTag("Hole2"))
            {
                SecondHoleAction();
            }
            else if (hit.collider.CompareTag("Hole3"))
            {
                ThirdHoleAction();
            }
        }
    }

    /// <summary>
    /// 드래그 처리 함수
    /// </summary>
    void OnDrag()
    {
        // 오브젝트를 드래그 중인 경우
        if (isDragging && draggedObject != null)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();     // 마우스 위치
            worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);   // 월드 좌표로 변환
            draggedObject.transform.position = worldPosition;               // 오브젝트 위치 갱신
        }
    }

    /// <summary>
    /// 위치에 따른 배치 처리 함수
    /// </summary>
    void OnRelease()
    {
        // 오브젝트를 드래그 중인 경우
        if (isDragging && draggedObject != null)
        {
            // 나무 막대 시작 부분에 도달한 후 통과 완료한 경우
            if (woodenSkewer.IsFinished)
            {
                draggedObject.transform.position = new Vector2(0.0f, worldPosition.y);  // 오브젝트 위치 설정
                onDeploy(draggedObject);                                                // 오브젝트 배치 완료
            }
            else
            {
                draggedObject.transform.position = originalPosition;                    // 오브젝트를 원래 위치로 되돌림
            }

            draggedObject = null;                                                       // 드래그 중인 오브젝트 해제
            isDragging = false;                                                         // 드래그 상태 비활성화
        }
    }

    /// <summary>
    /// 배치 완료 처리 함수
    /// </summary>
    /// <param name="draggedObject">처리할 오브젝트</param>
    void onDeploy(GameObject draggedObject)
    {
        // 예외 처리
        if (finishedObjectArray == null || draggedObject == null)
        {
            Debug.Log("finishedObjectArray 또는 draggedObject가 없습니다.");
            return;
        }

        // 배치 완료 처리
        for (int i = 0; i < finishedObjectArray.Length; i++)    // 인덱스가 낮은 순서대로
        {
            if (finishedObjectArray[i] == null)                 // 배열 중 null인 곳이 있는지 확인
            {
                finishedObjectArray[i] = draggedObject;         // 배치 완료된 오브젝트 삽입
                Debug.Log($"오브젝트가 추가된 인덱스 : {i}");         // 배치된 위치 출력
                return;
            }
        }

        // 변수 초기화
        woodenSkewer.isPassingThrough = false;
        woodenSkewer.isFinished = false;
    }

    /// <summary>
    /// 배치 취소 처리 함수
    /// </summary>
    void OnCancel()
    {
        // 예외 처리
        if (finishedObjectArray == null)
        {
            Debug.Log("finishedObjectArray가 없습니다.");
            return;
        }

        // 배치 취소 처리
        for (int i = finishedObjectArray.Length - 1; i >= 0; i--)
        {
            if (finishedObjectArray[i] != null)
            {
                finishedObjectArray[i].transform.position = originalPosition;    // 오브젝트를 원래 위치로 되돌림
                finishedObjectArray[i] = null;                                   // 드래그 중인 오브젝트 해제
                Debug.Log("배치 취소 완료");
                return;
            }
        }

        // 변수 초기화
        woodenSkewer.isPassingThrough = false;
        woodenSkewer.isFinished = false;
    }

    /// <summary>
    /// [구멍 1]에 대한 행동 처리 함수
    /// </summary>
    void FirstHoleAction()
    {
        Debug.Log("[Hole 1]을 클릭했습니다.");
    }

    /// <summary>
    /// [구멍 2]에 대한 행동 처리 함수
    /// </summary>
    void SecondHoleAction()
    {
        Debug.Log("[Hole 2]을 클릭했습니다.");
    }

    /// <summary>
    /// [구멍 3]에 대한 행동 처리 함수
    /// </summary>
    void ThirdHoleAction()
    {
        Debug.Log("[Hole 3]을 클릭했습니다.");
    }
}

