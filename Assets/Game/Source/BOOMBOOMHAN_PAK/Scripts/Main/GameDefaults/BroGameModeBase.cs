using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BroGameModeBase : GameModeBase
{
	private BombsPool bombsPool;

	private Brother brother;
	

	[SerializeField, GameModeProperty(Category = "主要", DisplayName = "音乐播放器")]
	private MusicPlayer player;

	[SerializeField, GameModeProperty(Category = "主要", DisplayName = "音乐事件收集者")]
	private MusicEventsCollector collector;

	[SerializeField, GameModeProperty(Category = "主要", DisplayName = "关卡序号")]
	private int levelOrder;

	[SerializeField, GameModeProperty(Category = "UI", DisplayName = "游戏结束UI")]
	private GameOverUi gameOverUi;

	[SerializeField, GameModeProperty(Category = "测试", DisplayName = "显示UI")]
	public TMP_Text Displayer;

	[SerializeField, GameModeProperty(Category = "测试", DisplayName = "日志")]
	public List<string> Logs;

	[SerializeField, GameModeProperty(Category = "评级", DisplayName = "A评级")]
	protected string gradeTextA;
	
	[SerializeField, GameModeProperty(Category = "评级", DisplayName = "B评级")]
	protected string gradeTextB;
	
	[SerializeField, GameModeProperty(Category = "评级", DisplayName = "C评级")]
	protected string gradeTextC;
	
	[SerializeField, GameModeProperty(Category = "评级", DisplayName = "D评级")]
	protected string gradeTextD;

	public BroGameModeBase()
	{
		gradeTextA = "C#";
		gradeTextB = "C++++--";
		gradeTextC = "C++";
		gradeTextD = "C";
	}

	private MusicEventBinaryTree eventTree;

	private float time;
	
	
	private int bombNum;

	private int score;

	private int injuredTimes;

	private BombHitResult result;

	private List<KeyValuePair<int, bool>> hitRecords;

	public UnityEvent<BombHitResult> HitEvent;

	public float PlayTime
	{
		//get => time;
		get => Mathf.Clamp(time, 0.0f, 9999.0f);
	}

	public BombsPool GetBombsPool()
	{
		return bombsPool;
	}

	public int LevelOrder
	{
		get => levelOrder;
	}
	
	protected override void Awake()
	{
		base.Awake();

		brother = DefaultCharacter as Brother;
		bombsPool = new BombsPool();

		bombNum = 0;
		score = 0;
		injuredTimes = 0;

		Logs = new List<string>(300);
		
		result = null;
		hitRecords = new List<KeyValuePair<int, bool>>();
		HitEvent = new UnityEvent<BombHitResult>();
	}

	private float initialBeat
	{
		get
		{
			//PlayerPrefs.SetFloat("INITIAL_BEAT", -1.0f);
			float val = PlayerPrefs.GetFloat("INITIAL_BEAT", 0.0f);
			if (val != 0.0f)
			{
				//AdvancedDebug.LogWarning("INITIAL_BEAT已被修改。");
				PlayerPrefs.SetFloat("INITIAL_BEAT", 0.0f);
			}
			PlayerPrefs.Save();

			return val;
		}
	}

	public MusicPlayer Player
	{
		get => player;
	}

	public MusicProperty Music
	{
		get => player.Music;
	}

	protected override void Start()
	{
		base.Start();

		float beat = initialBeat;
		float initialTime = Music.ToTime(beat);
		//AdvancedDebug.LogWarning(beat);
		AdvancedDebug.LogWarning(initialTime);

		const float readyDelay = 2.0f;
		time = initialTime - readyDelay;

		eventTree = new MusicEventBinaryTree(collector.GetMusicEventsArray(Music, levelOrder), this);

		PlayMusicAsync(readyDelay, initialTime);
		
		HitEvent.AddListener(Log);
		HitEvent.AddListener(JudgeResult);
	}

	void Log(BombHitResult result)
	{
		var records = result.Records;
		string dsp = "";
		foreach (var record in records)
		{
			dsp += $"{record},";
		}
		AdvancedDebug.Log($"{result.HasInjure}	{dsp}");
	}

	protected override void Update()
	{
		base.Update();

		time += Time.deltaTime;
		Displayer.text = 
			$"小节：{Music.Section(Player.Source) + 1}	拍：{1 + (int)Music.ToBeatInSection(Music.Beat(Player.Source))}	时间：{time}";
	}

	private async void PlayMusicAsync(float delay, float initialTime)
	{
		await UniTask.Delay(TimeSpan.FromSeconds(delay));

		player.PlayInTime(initialTime);
		eventTree.BeginExecute();
	}

	public virtual void HandleWhenBombArrives(Bomb bomb, int bombTrackIndex)
	{
		bombsPool.CollectBomb(bomb);
		//Logs.Add($"小节：{Music.Section(Player.Source) + 1}	拍：{1 + Music.ToBeatInSection(Music.Beat(Player.Source)) :F1}	时间：{time}");
		bombNum++;
		
		float broY = brother.transform.position.y;
		float distance = TrackSystem.Instance.Distance;
		float bottom = TrackSystem.Instance[3].transform.position.y;
		//AdvancedDebug.Log($"{broY}	{distance}	{bottom}");
		int broTrackIndex = 3;
		do
		{
			if (broY < bottom + 0.5f * distance)
			{
				break;
			}
			broTrackIndex--;
			
			if (broY < bottom + 1.5f * distance)
			{
				break;
			}
			broTrackIndex--;
			
			if (broY < bottom + 2.5f * distance)
			{
				break;
			}
			broTrackIndex--;
		} while (false);
		//AdvancedDebug.Log(result);

		KeyValuePair<int, bool> record = new KeyValuePair<int, bool>(bombTrackIndex, !ShouldInjureBrother(broTrackIndex, bombTrackIndex));
		hitRecords.Add(record);
	}

	protected override void LateUpdate()
	{
		if (hitRecords.Count != 0)
		{
			result = new BombHitResult(hitRecords.ToArray());
			HitEvent.Invoke(result);
			hitRecords.Clear();
		}
	}

	protected virtual void JudgeResult(BombHitResult result)
	{
		if (result.HasInjure)
		{
			injuredTimes++;
			brother.Injure(injuredTimes);
		}
		else
		{
			score++;
		}
	}

	protected virtual bool ShouldInjureBrother(int broTrackIndex, int bombTrackIndex) =>
		bombTrackIndex == broTrackIndex;

	protected virtual string Judge(float percentage)
	{
		if (percentage == 1.0f)
		{
			return gradeTextA;
		}

		if (percentage > 0.9f)
		{
			return gradeTextB;
		}

		if (percentage > 0.8f)
		{
			return gradeTextC;
		}

		return gradeTextD;
	}

	public override void OnVictory()
	{
		base.OnVictory();

		var ui = Instantiate(gameOverUi).GetComponent<GameOverUi>();
		AdvancedDebug.Log($"{score}	{bombNum}");
		string judge = Judge(score / (float)bombNum);
		ui.Level.text = judge;
		ui.SubTitle.text = $"你有{injuredTimes}个小姐";
	}

#if UNITY_EDITOR

	[GameModeProperty(Category = "测试", DisplayName = "初始小节")]
	public int InitialSection;

	[ContextMenu("设置初始拍")]
	public void SetInitialBeat()
	{
		float beat = Music.ToBeat(InitialSection - 1, 0.0f) - 0.0f;
		beat = Mathf.Clamp(beat, 0.0f, 999.0f);
		PlayerPrefs.SetFloat("INITIAL_BEAT", beat);
		PlayerPrefs.Save();
		AdvancedDebug.Log("初始拍已设置为" + beat);
	}

	[ContextMenu("测试")]
	public void Test()
	{

		AdvancedDebug.LogError(Music.CalculateBeginTimeFromEditor(2, 1.0f));
		AdvancedDebug.LogError(Music.CalculateBeginTimeFromEditor(6, 1.0f));
	}

#endif
}
