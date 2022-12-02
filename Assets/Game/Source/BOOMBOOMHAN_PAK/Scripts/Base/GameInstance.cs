using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameInstance : MonoBehaviour
{
	public static GameInstance Instance;
	
	[SerializeField]
	private GameAudioManager masterManager;
	
	protected virtual void Awake()
	{
		if (Instance) return;
		
		DontDestroyOnLoad(gameObject);
		Instance = this;

		masterManager = Instantiate(masterManager);
	}

	protected virtual void OnEnable()
	{
		
	}

	protected virtual void Start()
	{
		
	}

	protected virtual void Update()
	{
		
	}

	protected virtual void FixedUpdate()
	{
		
	}

	protected virtual void LateUpdate()
	{
		
	}

	protected virtual void OnDisable()
	{
		
	}

	protected virtual void OnDestroy()
	{
		
	}

	public static T InstanceCastTo<T>() where T : GameInstance
	{
		return Instance as T;
	}
}
