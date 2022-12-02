///
///
///
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUiController : MonoBehaviour
{
	[SerializeField, DisplayName("开始按钮")]
	private Button beginButton;

	[SerializeField, DisplayName("继续按钮")]
	private Button resumeButton;

	[SerializeField, DisplayName("设置按钮")]
	private Button settingsButton;

	[SerializeField, DisplayName("退出按钮")]
	private Button exitButton;

	[SerializeField, DisplayName("设置菜单控制器"), Space(15f)]
	private SettingsController settingsController;

	private Canvas canvas;
	
    public MainUiController()
    {
        
    }

    private void Start()
    {
	    canvas = GetComponent<Canvas>();
	    
	    beginButton.onClick.AddListener(BeginGame);
	    resumeButton.onClick.AddListener(ResumeGame);
	    settingsButton.onClick.AddListener(EnterSettingsPanel);
	    exitButton.onClick.AddListener(ExitGame);
    }

    void BeginGame()
    {
	    Debug.Log("开始游戏");
    }

    void ResumeGame()
    {
	    Debug.LogWarning("继续游戏");
    }

    void EnterSettingsPanel()
    {
	    var controller = Instantiate(settingsController);
	    controller.BackButton.onClick.AddListener(BackToMainUi);
	    canvas.enabled = false;
    }

    void BackToMainUi()
    {
	    canvas.enabled = true;
    }

    void ExitGame()
    {
#if UNITY_EDITOR
	    UnityEditor.EditorApplication.isPlaying = false;
#else
	    Application.Quit();
#endif
    }

    public void EnterLevel(string name)
    {
	    StartCoroutine(MaskedSceneCaster.CreateCast(name));
	    //SceneManager.LoadScene(name);
    }
}
