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
    /// 오브젝트의 원래 각도
    /// </summary>
    Quaternion originalRotation;

    /// <summary>
    /// 오브젝트의 실시간 월드 좌표
    /// </summary>
    Vector2 worldPosition;

    /// <summary>
    /// 선택된 오브젝트와 마우스 사이 거리
    /// </summary>
    public Vector2 offset;

    /// <summary>
    /// 드래그 중인 오브젝트
    /// </summary>
    GameObject draggedObject;

    /// <summary>
    /// 배치 완료된 오브젝트 배열
    /// </summary>
    GameObject[] finishedObjectArray;

    /// <summary>
    /// Shift 키가 눌렸는지 안눌렸는지 확인용 변수
    /// </summary>
    bool isShiftPressed = false;

    /// <summary>
    /// 꼬치 막대
    /// </summary>
    WoodenSkewer woodenSkewer;

    /// <summary>
    /// 꼬치 재료
    /// </summary>
    Ingredients ingredients;

    /// <summary>
    /// 플레이어 인풋
    /// </summary>
    PlayerInputActions inputActions;

    /// <summary>
    /// 메인 카메라
    /// </summary>
    public Camera mainCamera;

    private void Awake()
    {
        inputActions = new PlayerInputActions();            // 플레이어 인풋 액션
        mainCamera = Camera.main;                           // 메인 카메라 찾기

        woodenSkewer = FindAnyObjectByType<WoodenSkewer>(); // 꼬치 막대 찾기
        ingredients = FindAnyObjectByType<Ingredients>();   // 꼬치 재료 찾기

        finishedObjectArray = new GameObject[10];           // 오브젝트 배열 초기화
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.LeftClick.performed += OnSelect;
        inputActions.Player.LeftClick.canceled += OnRelease;
        inputActions.Player.RightClick.performed += OnCancel;

        inputActions.Player.HoleMode.performed += OnHoleModeOn;
        inputActions.Player.HoleMode.canceled += OnHoleModeOff;
    }

    private void OnDisable()
    {
        inputActions.Player.HoleMode.canceled -= OnHoleModeOff;
        inputActions.Player.HoleMode.performed -= OnHoleModeOn;

        inputActions.Player.RightClick.performed -= OnCancel;
        inputActions.Player.LeftClick.canceled -= OnRelease;
        inputActions.Player.LeftClick.performed -= OnSelect;

        inputActions.Player.Disable();
    }

    private void Update()
    {
        OnDrag();
    }

    private void OnHoleModeOn(InputAction.CallbackContext context)
    {
        isShiftPressed = true;
        Debug.Log("[ShiftPressed] 남은 구멍 중 하나를 선택하시오.");
    }

    private void OnHoleModeOff(InputAction.CallbackContext context)
    {
        isShiftPressed = false;
    }

    /// <summary>
    /// 선택 처리 함수
    /// </summary>
    private void OnSelect(InputAction.CallbackContext _)
    {
        // 마우스 클릭된 위치
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        // 레이 이용
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);               // 레이 생성
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);               // 클릭된 오브젝트를 감지

        // 충돌 확인
        if (hit.collider != null)
        {
            if (!isShiftPressed)                                                // Shift 키가 안 눌린 경우
            {
                // 선택한 오브젝트 저장
                GameObject clickedObject = hit.collider.gameObject;

                // 클릭된 오브젝트가 "Clickable" 태그를 가지고 있는지 확인
                if (clickedObject.CompareTag("Clickable"))
                {
                    offset = Vector2.zero;                                      // offset 초기화
                    draggedObject = clickedObject;                              // 드래그 오브젝트 설정
                    originalPosition = OriginalPosition(draggedObject);         // 원래 위치 저장
                    isDragging = true;                                          // 드래그 상태 활성화
                }
                // 클릭된 오브젝트가 "Hole1" 또는 "Hole2" 또는 "Hole3" 태그를 가지고 있는지 확인
                else if (clickedObject.CompareTag("Hole1") || clickedObject.CompareTag("Hole2") || clickedObject.CompareTag("Hole3"))
                {
                    Transform holeTransform = clickedObject.transform;
                    CircleCollider2D holeCollider = holeTransform.GetComponent<CircleCollider2D>();
                    offset = -holeCollider.offset;                              // offset 설정

                    draggedObject = holeTransform.parent.parent.gameObject;     // 드래그 오브젝트 설정
                    originalPosition = OriginalPosition(draggedObject);         // 원래 위치 저장
                    originalRotation = OriginalRotation(draggedObject);         // 원래 각도 저장
                    isDragging = true;                                          // 드래그 상태 활성화
                }

                // 각 구멍에 대한 추가 행동 처리
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
            else                                                                // Shift 키가 눌린 경우
            {
                // 선택한 구멍 저장
                GameObject clickedHole = hit.collider.gameObject;

                ////////////////////////////////////////////////////////// 선택한 구멍에 따라 프리팹 변경 처리
                if (!woodenSkewer.canChooseHole1)
                {
                    if (hit.collider.CompareTag("Hole1"))
                    {
                        Debug.Log("[Hole1] 선택 불가능");
                    }
                    else if(hit.collider.CompareTag("Hole2"))
                    {
                        // bool canFinishHole = true; /////////////////////////////
                    }
                    else if (hit.collider.CompareTag("Hole3"))
                    {
                        if (!woodenSkewer.canChooseHole2)
                        {
                            CheckLayer(clickedHole, Cheese.CheeseMode.ThreeHoles, Bacon.BaconMode.ThreeHoles);      // 스프라이트 변경
                            CheckHole(false, false, false);
                        }
                        else
                        {
                            Debug.Log("<<다른 구멍을 클릭해주세요.>>");
                        }
                    }
                }
                else if (!woodenSkewer.canChooseHole2) // 두번째 구멍을 배치 완료한 경우
                {
                    if (hit.collider.CompareTag("Hole1"))
                    {
                        CheckLayer(clickedHole, Cheese.CheeseMode.TwoHoles, Bacon.BaconMode.TwoHoles);              // 스프라이트 변경
                        ingredients.cheeseParent.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);                     // 스프라이트 y축 기준 180도 회전
                        CheckHole(false, false, true);
                    }
                    else if (hit.collider.CompareTag("Hole2"))
                    {
                        Debug.Log("[Hole2] 선택 불가능");
                    }
                    else if (hit.collider.CompareTag("Hole3"))
                    {
                        CheckLayer(clickedHole, Cheese.CheeseMode.TwoHoles, Bacon.BaconMode.TwoHoles);              // 스프라이트 변경
                        ingredients.cheeseParent.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);                   // 스프라이트 y축 기준 180도 회전
                        CheckHole(false, false, true);                                                              // 스프라이트가 회전했기 때문에 구멍3을 누르면 구멍1 변수가 false가 된다.
                    }
                }
                else if (!woodenSkewer.canChooseHole3)
                {
                    if (hit.collider.CompareTag("Hole1"))
                    {
                        Debug.Log("<<다른 구멍을 클릭해주세요.>>");
                    }
                    else if (hit.collider.CompareTag("Hole2"))
                    {

                    }
                    else if (hit.collider.CompareTag("Hole3"))
                    {
                        Debug.Log("[Hole3] 선택 불가능");
                    }
                }

                Debug.Log($"{woodenSkewer.canChooseHole1}, {woodenSkewer.canChooseHole2}, {woodenSkewer.canChooseHole3}");
            }
        }
    }

    /// <summary>
    /// 위치에 따른 배치 처리 함수
    /// </summary>
    private void OnRelease(InputAction.CallbackContext _)
    {
        // 오브젝트를 드래그 중인 경우
        if (isDragging && draggedObject != null)
        {
            // 나무 막대 시작 부분에 도달한 후 통과 완료한 경우
            if (woodenSkewer.IsFinished)
            {
                // 오브젝트 위치 설정
                if (draggedObject.CompareTag("Clickable"))
                {
                    draggedObject.transform.position = new Vector2(0.0f, worldPosition.y);
                }
                else
                {
                    /////////////////////////////////////////////// Shift 선택이 가능한 상태인지에 대한 변수 설정
                    if (draggedObject.layer == 6)
                    {
                        if (woodenSkewer.isHole1)
                        {
                            woodenSkewer.canChooseHole1 = false;
                        }
                        else if (woodenSkewer.isHole2)
                        {
                            woodenSkewer.canChooseHole2 = false;
                        }
                        else if (woodenSkewer.isHole3)
                        {
                            woodenSkewer.canChooseHole3 = false;
                        }
                    }
                    else if (draggedObject.layer == 7)
                    {
                        //////////////////////////////////////////////////// 치즈와 베이컨 bool 변수 나누기!!!!!!!!!!!!!!!!!!!!!
                        if (woodenSkewer.isHole1)
                        {
                            woodenSkewer.canChooseHole1 = false;
                        }
                        else if (woodenSkewer.isHole2)
                        {
                            woodenSkewer.canChooseHole2 = false;
                        }
                        else if (woodenSkewer.isHole3)
                        {
                            woodenSkewer.canChooseHole3 = false;
                        }
                    }

                    draggedObject.transform.position = new Vector2(offset.x, worldPosition.y);
                }
                OnDeploy(draggedObject);                                                        // 오브젝트 배치 완료
            }
            else
            {
                CheckLayer(draggedObject, Cheese.CheeseMode.Normal, Bacon.BaconMode.Normal);
                draggedObject.transform.position = originalPosition;                            // 오브젝트를 원래 위치로 되돌림
                draggedObject.transform.rotation = originalRotation;                            // 오브젝트를 원래 각도로 되돌림

                ResetSkewer();                                                                  // 변수 초기화
                woodenSkewer.canChooseHole1 = true;                                             // 선택 가능 구멍 초기화
                woodenSkewer.canChooseHole2 = true;
                woodenSkewer.canChooseHole3 = true;
            }

            draggedObject = null;                                                               // 드래그 중인 오브젝트 해제
            isDragging = false;                                                                 // 드래그 상태 비활성화
        }
    }

    /// <summary>
    /// 배치 완료 처리 함수
    /// </summary>
    /// <param name="draggedObject">처리할 오브젝트</param>
    void OnDeploy(GameObject draggedObject)
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

        ResetSkewer();                                          // 변수 초기화
    }

    /// <summary>
    /// 배치 취소 처리 함수
    /// </summary>
    private void OnCancel(InputAction.CallbackContext _)
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
                CheckLayer(finishedObjectArray[i], Cheese.CheeseMode.Normal, Bacon.BaconMode.Normal);

                originalPosition = OriginalPosition(finishedObjectArray[i]);    // 원래 위치 재설정
                originalRotation = OriginalRotation(finishedObjectArray[i]);    // 원래 각도 재설정
                finishedObjectArray[i].transform.position = originalPosition;   // 오브젝트를 원래 위치로 되돌림
                finishedObjectArray[i] = null;                                  // 배치 취소된 오브젝트는 배열에서 삭제

                Debug.Log($"배치 취소된 인덱스 : {i}");                             // 배치 취소된 인덱스 출력
                break;
            }
        }

        // 선택 가능 구멍 초기화
        woodenSkewer.canChooseHole1 = true;
        woodenSkewer.canChooseHole2 = true;
        woodenSkewer.canChooseHole3 = true;
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
            draggedObject.transform.position = worldPosition + offset;      // 오브젝트 위치 갱신
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
            case "Cheese":
                originalPosition = ingredients.cheesePosition;
                break;
            case "Bacon":
                originalPosition = ingredients.baconPosition;
                break;
        }

        return originalPosition;
    }

    /// <summary>
    /// 꼬치 재료의 원래 각도를 반환해주는 함수
    /// </summary>
    /// <param name="obj">원래 각도를 확인하고 싶은 오브젝트</param>
    /// <returns>오브젝트의 원래 각도</returns>
    Quaternion OriginalRotation(GameObject obj)
    {
        switch (obj.name)
        {
            case "Cheese":
                originalRotation = ingredients.cheeseRotation;
                break;
            case "Bacon":
                originalRotation = ingredients.baconRotation;
                break;
            default:
                // Debug.Log($"[{obj.name}] 각도 유지");
                break;
        }

        return originalRotation;
    }

    /// <summary>
    /// 초기화를 위한 오브젝트 레이어 비교 함수
    /// </summary>
    /// <param name="obj">확인할 오브젝트</param>
    /// <param name="cheeseState">치즈의 상태</param>
    /// <param name="baconState">베이컨의 상태</param>
    void CheckLayer(GameObject obj, Cheese.CheeseMode cheeseState, Bacon.BaconMode baconState)
    {
        if (obj.layer == 6)                                 // 해당 오브젝트가 [치즈]인 경우
        {
            ingredients.cheese.CheeseState = cheeseState;   // 치즈 모드 초기화
            if (cheeseState == Cheese.CheeseMode.Normal)
            {
                ingredients.cheeseParent.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
        }
        else if (obj.layer == 7)                            // 해당 오브젝트가 [베이컨]인 경우
        {
            ingredients.bacon.BaconState = baconState;      // 베이컨 모드 초기화
            if (baconState == Bacon.BaconMode.Normal)
            {
                ingredients.baconParent.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
        }
    }

    /// <summary>
    /// 구멍 선택에 대한 저장용 함수
    /// </summary>
    /// <param name="one">첫번째 구멍 선택 가능 여부</param>
    /// <param name="two">두번째 구멍 선택 가능 여부</param>
    /// <param name="three">세번째 구멍 선택 가능 여부</param>
    void CheckHole(bool one, bool two, bool three)
    {
        woodenSkewer.canChooseHole1 = one;
        woodenSkewer.canChooseHole2 = two;
        woodenSkewer.canChooseHole3 = three;
    }

    /// <summary>
    /// woodenSkewer의 변수를 초기화하는 함수
    /// </summary>
    void ResetSkewer()
    {
        // 통과 가능 여부 초기화
        woodenSkewer.isPassingThrough = false;
        woodenSkewer.isFinished = false;

        // 구멍 선택 여부 초기화
        woodenSkewer.isHole1 = false;
        woodenSkewer.isHole2 = false;
        woodenSkewer.isHole3 = false;

        // Shift 선택 상태 초기화
        //woodenSkewer.canChooseHole1 = true;
        //woodenSkewer.canChooseHole2 = true;
        //woodenSkewer.canChooseHole3 = true;
    }

    /// <summary>
    /// [구멍 1]에 대한 행동 처리 함수
    /// </summary>
    void FirstHoleAction()
    {
        woodenSkewer.isHole1 = true;
        woodenSkewer.isHole2 = false;
        woodenSkewer.isHole3 = false;
        Debug.Log("[Hole 1]을 클릭했습니다.");
    }

    /// <summary>
    /// [구멍 2]에 대한 행동 처리 함수
    /// </summary>
    void SecondHoleAction()
    {
        woodenSkewer.isHole1 = false;
        woodenSkewer.isHole2 = true;
        woodenSkewer.isHole3 = false;
        Debug.Log("[Hole 2]을 클릭했습니다.");
    }

    /// <summary>
    /// [구멍 3]에 대한 행동 처리 함수
    /// </summary>
    void ThirdHoleAction()
    {
        woodenSkewer.isHole1 = false;
        woodenSkewer.isHole2 = false;
        woodenSkewer.isHole3 = true;
        Debug.Log("[Hole 3]을 클릭했습니다.");
    }

    /// <summary>
    //////////////////////////////////////////////////////////////////////////// [완료] 버튼을 누른 경우 처리 함수
    /// </summary>
    void isTheEnd()
    {
        // 접시까지의 거리 (y좌표)
        float endPosition = 10.0f;

        // 꼬치 막대 위치 이동
        Vector2 skewerPosition = woodenSkewer.transform.position;
        skewerPosition = new Vector2(0, skewerPosition.y + endPosition * Time.deltaTime);

        // 배치 완료된 재료들 위치 이동
        for (int i = 0; i < finishedObjectArray.Length; i++)
        {
            if (finishedObjectArray[i] != null)
            {
                Vector2 ingredientPosition = finishedObjectArray[i].transform.position;
                ingredientPosition = new Vector2(0, ingredientPosition.y + endPosition * Time.deltaTime);
            }
            else
            {
                Debug.Log("[버튼] 모든 재료 배치 완료");
                break;
            }
        }
    }
}

