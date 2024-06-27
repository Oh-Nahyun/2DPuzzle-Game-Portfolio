using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    /// <summary>
    /// [BACK] 버튼
    /// </summary>
    Button backButton;

    /// <summary>
    /// 텍스트 패널 오브젝트
    /// </summary>
    public GameObject textPanel;

    private void Awake()
    {
        backButton = GetComponent<Button>();

        // [BACK] 버튼이 눌려지면 AddListener로 등록한 함수 실행
        backButton.onClick.AddListener(() =>
        {
            textPanel.SetActive(false); // 텍스트 패널 비활성화
        });
    }
}
