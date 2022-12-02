using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restarter : MonoBehaviour
{
	[SerializeField]
	private GameObject restarterUiResource;
	
	private GameObject restarterUi;
	
	private void Awake()
	{
		Time.timeScale = 1.0f;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R) && (!restarterUi))
		{
			Time.timeScale = 0.0f;
			//GameModeBase.Get<BroGameModeBase>().IsMusicPlaying = false;
			restarterUi = Instantiate(restarterUiResource);
			TMP_InputField inputField = restarterUi.GetComponentInChildren<TMP_InputField>();
			Button button = restarterUi.GetComponentInChildren<Button>();
			button.onClick.AddListener((() => Restart(float.Parse(inputField.text))));
		}
	}

	void Restart(float beat)
	{
		Destroy(restarterUi);
		Time.timeScale = 1.0f;
		PlayerPrefs.SetFloat("BEAT", beat);
		SceneManager.LoadScene("Main");
		//(GameModeBase.Instance as BroGameModeBase).RevertMusic(beat);
		/*TimerManager.GetTimerManager()
			.SetTimer((() =>
			{
				(GameModeBase.Instance as BroGameModeBase).MusicPlayer.RevertTo(beat);
				Debug.LogWarning("Restart");
			}), 0.0f, 4.0f, 1L);
		SceneManager.LoadScene("Main");*/

	}
}
