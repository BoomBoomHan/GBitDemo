using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MusicPlayer : MonoBehaviour
{
	private AudioSource mainSource;

	private AudioSource backupSource;

	[SerializeField, DisplayName("音频")]
	private AudioClip bgm;

	[SerializeField]
	private MusicProperty musicProperty;

	[SerializeField, DisplayName("音量大小"), Range(0.0f, 1.0f)]
	private float volume;

	public MusicProperty Music
	{
		get => musicProperty;
	}

	public AudioSource Source
	{
		get => mainSource;
	}

	public MusicPlayer()
	{
		volume = 1.0f;
	}

	private void Awake()
	{
		mainSource = gameObject.AddComponent<AudioSource>();
		backupSource = gameObject.AddComponent<AudioSource>();
		mainSource.clip = bgm;
		mainSource.volume = volume;
		backupSource.volume = volume;
	}

	private void Start()
	{
		/*TimerManager.GetTimerManager().SetTimer(ChangeColorToPurple, 0.0f, musicProperty.ToTime(19, 4.0f), 1L);
		TimerManager.GetTimerManager().SetTimer(RevertToEnd, 0.0f, musicProperty.ToTime(28, 4.0f), 1L);*/
		//AdvancedDebug.Log( "1小节1拍="+ musicProperty.ToTime(0, 0.0f));
	}

	private void Update()
	{
		
	}

	public void Play()
	{
		if (musicProperty.PlayDelayInSeconds > 0.0f)
		{
			mainSource.time = musicProperty.PlayDelayInSeconds;
			mainSource.Play();
		}
		else
		{
			mainSource.PlayDelayed(musicProperty.PlayDelayInSeconds);
		}
	}

	public void PlayInTime(float initialTime)
	{
		//AdvancedDebug.LogWarning(Time.time);
		mainSource.time = initialTime;
		mainSource.Play();
	}

	public float PlayTime
	{
		get => mainSource.time;
		set => mainSource.time = value;
	}

	public void RevertTo(float time)
	{
		backupSource.clip = mainSource.clip;
		backupSource.time = mainSource.time;
		backupSource.volume = mainSource.volume;
		backupSource.Play();
		backupSource.DOFade(0.0f, musicProperty.ToDuration(0.5f));
		mainSource.time = time;
	}

	void ChangeColorToPurple()
	{
		Camera.main.DOColor(new Vector4(210.0f, 0.0f, 181.0f), 2.0f);
	}

	void RevertToEnd()
	{
		RevertTo(musicProperty.ToTime(78, 4.0f));
	}
}
