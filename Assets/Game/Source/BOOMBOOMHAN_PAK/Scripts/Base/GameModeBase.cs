using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[DisallowMultipleComponent]
public class GameModeBase : MonoBehaviour
{
	public static GameModeBase Instance;

	[SerializeField, GameModeProperty(Category = "绑定", DisplayName = "默认角色类")]
	protected Character defaultCharacter;
	
	protected float fps;

	public float Fps
	{
		get
		{
			return fps;
		}
	}

	protected float ping;

	public float GamePing
	{
		get
		{
			return ping;
		}
	}

	public Character DefaultCharacter
	{
		get
		{
			return defaultCharacter;
		}
		protected set
		{
			defaultCharacter = value;
		}
	}

	[SerializeField, GameModeProperty(Category = "绑定", DisplayName = "默认玩家控制器类")]
	protected PlayerController defaultPlayerController;

	public PlayerController DefaultPlayerController
	{
		get
		{
			return defaultPlayerController;
		}
		protected set
		{
			defaultPlayerController = value;
		}
	}

	[SerializeField, GameModeProperty(Category = "绑定", DisplayName = "默认游戏状态类")]
	protected GameState defaultGameState;

	public GameState DefaultGameState
	{
		get
		{
			return defaultGameState;
		}
		protected set
		{
			defaultGameState = value;
		}
	}
	
	[SerializeField, GameModeProperty(Category = "绑定", DisplayName = "默认玩家状态类")]
	protected PlayerState defaultPlayerState;

	public PlayerState DefaultPlayerState
	{
		get
		{
			return defaultPlayerState;
		}
		protected set
		{
			defaultPlayerState = value;
		}
	}

	[SerializeField, GameModeProperty(Category = "绑定", SpaceFromLast = 10f, DisplayName = "游戏实例类")]
	protected GameInstance gameInstance;
	
	[GameModeProperty(DisplayName = "玩家起始点")]
	public Vector2 PlayerStartPosition;

	[SerializeField, GameModeProperty(DisplayName = "有胜利或失败条件")]
	private bool canWinOrLoss;

	private int levelProcess;

	public bool IsWin
	{
		get => levelProcess == 1;
	}
	
	public bool IsLoss
	{
		get => levelProcess == -1;
	}

	private IntChangedEventsWithPresets processEvents;

	public GameModeBase()
	{
		fps = 0f;
		ping = 0f;
		levelProcess = 0;
		canWinOrLoss = true;
	}

	protected virtual void Awake()
	{
		if (Instance)
		{
			Debug.LogError("Fatal!");
			Debug.LogError("GameModeBase存在多个实例！");
		}

		Instance = this;
		InstantiateDefaults(ref defaultCharacter, PlayerStartPosition);
		InstantiateDefaults(ref defaultPlayerController);
		InstantiateDefaults(ref defaultGameState);
		InstantiateDefaults(ref defaultPlayerState);
		if (!GameInstance.Instance)
		{
			InstantiateDefaults(ref gameInstance);
		}
		
		defaultPlayerController.BindCharacter(defaultCharacter);
		defaultPlayerController.BindPlayerState(defaultPlayerState);
		defaultPlayerState.BindCharacter(defaultCharacter);
		
		Random.InitState((int) System.DateTime.Now.Ticks);
		Application.targetFrameRate = -1;
	}

	protected virtual void OnEnable()
	{
		if (canWinOrLoss)
		{
			processEvents = new IntChangedEventsWithPresets(ref levelProcess, new CommonPresetEventList<int>(1, OnVictory),
				new CommonPresetEventList<int>(-1, OnDefeat));
			processEvents.BeginMonitor();
		}
	}

	protected virtual void Start()
	{
		
	}

	private float deltaTime = 0f;
    protected virtual void Update()
    {
	    deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	    fps = 1f / deltaTime;
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
	    StopAllCoroutines();
    }
    
    protected void InstantiateDefaults<T>(ref T obj) where T : Behaviour
    {
	    InstantiateDefaults(ref obj, Vector3.zero, Quaternion.identity);
    }

    protected void InstantiateDefaults<T>(ref T obj, Vector3 location) where T : Behaviour
    {
	    InstantiateDefaults(ref obj, location, Quaternion.identity);
    }
    
    protected void InstantiateDefaults<T>(ref T obj, Vector3 location, Quaternion rotation) where T : Behaviour
    {
	    if (!obj.isActiveAndEnabled)
	    {
		    obj = Instantiate(obj, location, rotation);
	    }
    }

    public void Victory()
    {
	    levelProcess = 1;
    }

    public void Defeat()
    {
	    levelProcess = -1;
    }

    public virtual void OnVictory()
    {
	    Debug.Log("Victory");
	    DefaultPlayerController.DisableInput();
    }

    public virtual void OnDefeat()
    {
	    Debug.LogWarning("Defeat");
	    DefaultPlayerController.DisableInput();
    }

    public void MaskedCast(string sceneName)
    {
	    StartCoroutine(MaskedSceneCaster.CreateCast(sceneName));
    }

    public static T Get<T>() where T : GameModeBase
    {
	    return Instance as T;
    }
}
