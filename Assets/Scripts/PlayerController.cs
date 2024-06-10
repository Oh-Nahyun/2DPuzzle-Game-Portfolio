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
    /// 드래그 중인 오브젝트
    /// </summary>
    GameObject draggedObject;

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
        playerInputAction = new PlayerInputActions();
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
        // 오브젝트를 드래그 중인 경우
        if (isDragging && draggedObject != null)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();             // 마우스 위치
            Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);   // 월드 좌표로 변환
            draggedObject.transform.position = worldPosition;                       // 오브젝트 위치 갱신
        }
    }

    /// <summary>
    /// 마우스 왼쪽 버튼 클릭 처리 함수
    /// </summary>
    private void OnLeftClickInput(InputAction.CallbackContext context)
    {
        if (!context.canceled)
        {
            OnSelect();
        }
        else
        {
            OnCancel();
        }
    }

    /// <summary>
    /// 마우스 오른쪽 버튼 클릭 처리 함수
    /// </summary>
    /// <param name="context"></param>
    private void OnRightClickInput(InputAction.CallbackContext context)
    {
        if (!context.canceled)
        {
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
            // 오브젝트 저장
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
    /// 취소 처리 함수
    /// </summary>
    void OnCancel()
    {
        if (isDragging && draggedObject != null)
        {
            draggedObject.transform.position = originalPosition;    // 오브젝트를 원래 위치로 되돌림
            draggedObject = null;                                   // 드래그 중인 오브젝트 해제
            isDragging = false;                                     // 드래그 상태 비활성화
        }
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

