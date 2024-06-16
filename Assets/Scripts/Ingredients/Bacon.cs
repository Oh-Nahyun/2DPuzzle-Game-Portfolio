using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacon : MonoBehaviour
{
    /// <summary>
    /// 베이컨 모드
    /// </summary>
    public enum BaconMode
    {
        Normal = 0,
        OneHole,
        TwoHoles,
        ThreeHoles
    }

    /// <summary>
    /// 베이컨 모드 상태
    /// </summary>
    BaconMode baconState = BaconMode.Normal;

    /// <summary>
    /// 베이컨 모드 상태 확인용 프로퍼티
    /// </summary>
    public BaconMode BaconState
    {
        get => baconState;
    }

    // 베이컨 프리팹
    GameObject Bacon_0;
    GameObject Bacon_1;
    GameObject Bacon_2;
    GameObject Bacon_3;

    private void Awake()
    {
        // 베이컨 프리팹 찾기
        Bacon_0 = transform.GetChild(0).gameObject;
        Bacon_1 = transform.GetChild(1).gameObject;
        Bacon_2 = transform.GetChild(2).gameObject;
        Bacon_3 = transform.GetChild(3).gameObject;

        BaconModeChange(BaconState);
    }

    /// <summary>
    /// 베이컨 프리팹 활성화 여부 결정용 함수
    /// </summary>
    /// <param name="mode">베이컨 모드</param>
    void BaconModeChange(BaconMode mode)
    {
        switch (mode)
        {
            case BaconMode.Normal:
                Bacon_0.SetActive(true);
                Bacon_1.SetActive(false);
                Bacon_2.SetActive(false);
                Bacon_3.SetActive(false);
                break;
            case BaconMode.OneHole:
                Bacon_0.SetActive(false);
                Bacon_1.SetActive(true);
                Bacon_2.SetActive(false);
                Bacon_3.SetActive(false);
                break;
            case BaconMode.TwoHoles:
                Bacon_0.SetActive(false);
                Bacon_1.SetActive(false);
                Bacon_2.SetActive(true);
                Bacon_3.SetActive(false);
                break;
            case BaconMode.ThreeHoles:
                Bacon_0.SetActive(false);
                Bacon_1.SetActive(false);
                Bacon_2.SetActive(false);
                Bacon_3.SetActive(true);
                break;
        }
    }
}
