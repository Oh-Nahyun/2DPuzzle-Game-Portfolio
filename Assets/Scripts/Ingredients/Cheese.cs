using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour
{
    /// <summary>
    /// 치즈 모드
    /// </summary>
    public enum CheeseMode
    {
        Normal = 0,
        OneHole,
        TwoHoles,
        ThreeHoles,
        NewTwoHoles,
        NewThreeHoles
    }

    /// <summary>
    /// 치즈 모드 상태
    /// </summary>
    CheeseMode cheeseState = CheeseMode.Normal;

    /// <summary>
    /// 치즈 모드 상태 확인 및 설정용 프로퍼티
    /// </summary>
    public CheeseMode CheeseState
    {
        get => cheeseState;
        set
        {
            if (value != cheeseState)
            {
                cheeseState = value;
            }
        }
    }

    // 치즈 프리팹
    GameObject Cheese_0;
    public GameObject Cheese_1;
    GameObject Cheese_2;
    GameObject Cheese_3;
    GameObject Cheese_4;
    GameObject Cheese_5;

    private void Awake()
    {
        // 치즈 프리팹 찾기
        Cheese_0 = transform.GetChild(0).gameObject;
        Cheese_1 = transform.GetChild(1).gameObject;
        Cheese_2 = transform.GetChild(2).gameObject;
        Cheese_3 = transform.GetChild(3).gameObject;
        Cheese_4 = transform.GetChild(4).gameObject;
        Cheese_5 = transform.GetChild(5).gameObject;

        CheeseModeChange(CheeseState);
    }

    private void Update()
    {
        CheeseModeChange(CheeseState);
    }

    /// <summary>
    /// 치즈 프리팹 활성화 여부 결정용 함수
    /// </summary>
    /// <param name="mode">치즈 모드</param>
    void CheeseModeChange(CheeseMode mode)
    {
        switch (mode)
        {
            case CheeseMode.Normal:
                Cheese_0.SetActive(true);
                Cheese_1.SetActive(false);
                Cheese_2.SetActive(false);
                Cheese_3.SetActive(false);
                Cheese_4.SetActive(false);
                Cheese_5.SetActive(false);
                break;
            case CheeseMode.OneHole:
                Cheese_0.SetActive(false);
                Cheese_1.SetActive(true);
                Cheese_2.SetActive(false);
                Cheese_3.SetActive(false);
                Cheese_4.SetActive(false);
                Cheese_5.SetActive(false);
                break;
            case CheeseMode.TwoHoles:
                Cheese_0.SetActive(false);
                Cheese_1.SetActive(false);
                Cheese_2.SetActive(true);
                Cheese_3.SetActive(false);
                Cheese_4.SetActive(false);
                Cheese_5.SetActive(false);
                break;
            case CheeseMode.ThreeHoles:
                Cheese_0.SetActive(false);
                Cheese_1.SetActive(false);
                Cheese_2.SetActive(false);
                Cheese_3.SetActive(true);
                Cheese_4.SetActive(false);
                Cheese_5.SetActive(false);
                break;
            case CheeseMode.NewTwoHoles:
                Cheese_0.SetActive(false);
                Cheese_1.SetActive(false);
                Cheese_2.SetActive(false);
                Cheese_3.SetActive(false);
                Cheese_4.SetActive(true);
                Cheese_5.SetActive(false);
                break;
            case CheeseMode.NewThreeHoles:
                Cheese_0.SetActive(false);
                Cheese_1.SetActive(false);
                Cheese_2.SetActive(false);
                Cheese_3.SetActive(false);
                Cheese_4.SetActive(false);
                Cheese_5.SetActive(true);
                break;
        }
    }
}
