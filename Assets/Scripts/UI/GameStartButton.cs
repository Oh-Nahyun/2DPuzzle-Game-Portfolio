using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartButton : MonoBehaviour
{
    /// <summary>
    /// 다음에 타이틀씬이 끝나고 나서 불려질 씬의 이름
    /// </summary>
    public string nextSceneName = "Loading";

    /// <summary>
    /// [GAME START] 버튼
    /// </summary>
    Button startButton;

    private void Awake()
    {
        startButton = GetComponent<Button>();

        // [GAME START] 버튼이 눌려지면 AddListener로 등록한 함수 실행
        startButton.onClick.AddListener(() =>
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
