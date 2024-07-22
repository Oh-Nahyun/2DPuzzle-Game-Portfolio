using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndButton : MonoBehaviour
{
    /// <summary>
    /// 씬이 끝나고 불려질 씬의 이름
    /// </summary>
    public string nextSceneName = "GameTitle";

    /// <summary>
    /// [Game End] 버튼
    /// </summary>
    Button endButton;

    private void Awake()
    {
        endButton = GetComponent<Button>();

        // [Game End] 버튼이 눌려지면 AddListener로 등록한 함수 실행
        endButton.onClick.AddListener(() =>
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
