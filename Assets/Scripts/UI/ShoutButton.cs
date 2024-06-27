using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoutButton : MonoBehaviour
{
    /// <summary>
    /// [SHOUT] 버튼
    /// </summary>
    Button shoutButton;

    /// <summary>
    /// 텍스트 패널 오브젝트
    /// </summary>
    public GameObject textPanel;

    private void Awake()
    {
        shoutButton = GetComponent<Button>();

        // [SHOUT] 버튼이 눌려지면 AddListener로 등록한 함수 실행
        shoutButton.onClick.AddListener(() =>
        {
            textPanel.SetActive(true); // 텍스트 패널 활성화
        });
    }
}
