using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TryAgainButton : MonoBehaviour
{
    /// <summary>
    /// 씬이 끝나고 불려질 씬의 이름
    /// </summary>
    public string nextSceneName = "Loading";

    /// <summary>
    /// [Try Again] 버튼
    /// </summary>
    Button tryAgainButton;

    private void Awake()
    {
        tryAgainButton = GetComponent<Button>();

        // [Try Again] 버튼이 눌려지면 AddListener로 등록한 함수 실행
        tryAgainButton.onClick.AddListener(() =>
        {
            LoadScene();
        });
    }

    /// <summary>
    /// 다음 씬을 로드하는 함수
    /// </summary>
    private void LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
