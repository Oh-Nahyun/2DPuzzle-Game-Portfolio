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
    /// 플레이어 인풋
    /// </summary>
    PlayerInputActions playerInputAction;

    /// <summary>
    /// 꼬치 막대
    /// </summary>
    WoodenSkewer woodenSkewer;

    /// <summary>
    /// 꼬치 재료
    /// </summary>
    Ingredients ingredients;

    /// <summary>
    /// 메인 카메라
    /// </summary>
    public Camera mainCamera;

    private void Awake()
    {
        playerInputAction = new PlayerInputActions();       // 플레이어 인풋 액션
        woodenSkewer = FindAnyObjectByType<WoodenSkewer>(); // 꼬치 막대 찾기
        ingredients = FindAnyObjectByType<Ingredients>();   // 꼬치 재료 찾기
        mainCamera = Camera.main;                           // 메인 카메라 찾기

        finishedObjectArray = new GameObject[10];           // 오브젝트 배열 초기화
    }

    private void OnEnable()
    {
        playerInputAction.Player.Enable();

        playerInputAction.Player.Select.performed += OnLeftClickInput;
        playerInputAction.Player.Select.canceled += OnLeftClickInput;

        playerInputAction.Player.Cancel.performed += OnRightClickInput;
    }

    private void OnDisable()
    {
        playerInputAction.Player.Cancel.performed -= OnRightClickInput;

        playerInputAction.Player.Select.canceled -= OnLeftClickInput;
        playerInputAction.Player.Select.performed -= OnLeftClickInput;

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
        if (!context.canceled)
        {
            // 마우스 오른쪽 버튼을 누른 경우
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
                originalPosition = OriginalPosition(draggedObject); // 원래 위치 저장
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
                ResetSkewer();
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
        for (int i = 0; i < finishedObjectArray.Length; i++)    // 인덱스가 작은 순서대로 확인
        {
            if (finishedObjectArray[i] == null)                 // 배열 중 null인 곳이 있는 경우
            {
                finishedObjectArray[i] = draggedObject;         // 배치 완료된 오브젝트 삽입
                Debug.Log($"배치 완료된 인덱스 : {i}");             // 배치 완료된 인덱스 출력
                break;
            }
        }

        ResetSkewer();
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
        for (int i = finishedObjectArray.Length - 1; i >= 0; i--)               // 인덱스가 큰 순서대로 확인
        {
            if (finishedObjectArray[i] != null)                                 // 배열 중 null이 아닌 곳이 있는 경우
            {
                originalPosition = OriginalPosition(finishedObjectArray[i]);    // 원래 위치 재설정
                finishedObjectArray[i].transform.position = originalPosition;   // 오브젝트를 원래 위치로 되돌림
                finishedObjectArray[i] = null;                                  // 배치 취소된 오브젝트는 배열에서 삭제
                Debug.Log($"배치 취소된 인덱스 : {i}");                             // 배치 취소된 인덱스 출력
                break;
            }
        }
    }

    /// <summary>
    /// 꼬치 재료의 원래 위치를 반환해주는 함수
    /// </summary>
    /// <param name="obj">원래 위치를 확인하고 싶은 오브젝트</param>
    /// <returns>오브젝트의 원래 위치</returns>
    Vector2 OriginalPosition(GameObject obj)
    {
        switch (obj.name)
        {
            case "Meat":
                originalPosition = ingredients.meatPosition;
                break;
            case "Pepper":
                originalPosition = ingredients.pepperPosition;
                break;
            case "Shrimp":
                originalPosition = ingredients.shrimpPosition;
                break;
            case "Tomato":
                originalPosition = ingredients.tomatoPosition;
                break;
        }

        return originalPosition;
    }

    /// <summary>
    /// woodenSkewer의 변수를 초기화하는 함수
    /// </summary>
    void ResetSkewer()
    {
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

