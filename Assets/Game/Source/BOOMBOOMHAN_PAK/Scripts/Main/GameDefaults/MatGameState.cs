using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MatGameState : GameState
{
	MatGameModeBase gmb;

	GameUi ui;

	class PlayerComponentGroup
	{
		public MatCharacter Mc;
		public MatPlayerController Mpc;
		public MatPlayerState Mps;

		public PlayerComponentGroup(MatCharacter mc, MatPlayerController mpc, MatPlayerState mps)
		{
			Mc = mc;
			Mpc = mpc;
			Mps = mps;
		}
	}

	private SupplyFloor[] lefts;
	private SupplyFloor[] rights;

	PlayerComponentGroup actPlayer;
	PlayerComponentGroup waitPlayer;

	IntVector2D damagePointThisRound = new IntVector2D(-1, -1);
	IntVector2D damagePointNextRound;

	[SerializeField]
	AudioSource source;

	[SerializeField]
	AudioClip clip;


	protected override async void Start()
	{
		base.Start();

		source.clip = clip;

		
		gmb = GameModeBase.Get<MatGameModeBase>();
		ui = gmb.Ui; AdvancedDebug.LogWarning(gmb != null);
		lefts = new SupplyFloor[] { gmb.MatSystem[1, 1] as SupplyFloor, gmb.MatSystem[1, 4] as SupplyFloor, gmb.MatSystem[4, 1] as SupplyFloor, gmb.MatSystem[4, 4] as SupplyFloor, };
		rights = new SupplyFloor[] { gmb.MatSystem[1, 7] as SupplyFloor, gmb.MatSystem[1, 10] as SupplyFloor, gmb.MatSystem[4, 7] as SupplyFloor, gmb.MatSystem[4, 10] as SupplyFloor, };
		bottomOrigin = ui.BottomText.rectTransform.position;
		actPlayer = new PlayerComponentGroup(gmb.P1Character, gmb.P1Controller, gmb.P1State);
		waitPlayer = new PlayerComponentGroup(gmb.P2Character, gmb.P2Controller, gmb.P2State);

		actPlayer.Mc.MoveBegin.AddListener(JudgeIfRoundEnd);
		waitPlayer.Mc.MoveBegin.AddListener(JudgeIfRoundEnd);

		ResetPoints();
		GenerateSupplies(2);

		await OnBattleBegin();
		OnRoundBegin();
	}

	protected override void Update()
	{
		base.Update();

		if (actPlayer != null)
		{
			ui.RemainStepsCount.text = actPlayer.Mps.RemainingStepCount.ToString();
		}

		if (ui)
		{
			ui.BluePreview.text = $"±æªÿ∫œ£∫{damagePointThisRound + new IntVector2D(1, 1)}£¨…À∫¶£∫{Game.ComputeDamage(gmb.MatSystem, EPlayerTeam.Blue, damagePointThisRound)}";
			ui.RedPreview.text = $"±æªÿ∫œ£∫{Game.ToMirrorLocation(gmb.MatSystem, damagePointThisRound) + new IntVector2D(1, 1)}£¨…À∫¶£∫{Game.ComputeDamage(gmb.MatSystem, EPlayerTeam.Red, Game.ToMirrorLocation(gmb.MatSystem, damagePointThisRound))}";
		}
	}

	async UniTask OnBattleBegin()
	{
		await UniTask.Delay(3000);
	}	

	void OnRoundBegin()
	{
		EPlayerTeam actTeam = actPlayer.Mc.Team;
		ui.RemainStepsLabel.color = ui.GetColor(actTeam);
		ui.RemainStepsCount.color = ui.GetColor(actTeam);

		actPlayer.Mpc.EnableInput();
		actPlayer.Mps.OnRoundBegin();
	}

	async void JudgeIfRoundEnd(IntVector2D vec)
	{
		await UniTask.Delay(TimeSpan.FromSeconds(Time.deltaTime));
		//AdvancedDebug.LogWarning($"{gmb.P1State.RemainingStepCount}	{gmb.P2State.RemainingStepCount}	{actPlayer.Mc == gmb.P1Character}");
		if (actPlayer.Mps.RemainingStepCount == 0)
		{
			OnRoundEnd();
		}
	}

	async void OnRoundEnd()
	{
		roundCount++;
		ui.RemainStepsLabel.color = Color.clear;
		ui.RemainStepsCount.color = Color.clear;
		//AdvancedDebug.LogWarning($"{actPlayer.Mc.Team} RoundEnd");
		actPlayer.Mps.OnRoundEnd();
		actPlayer.Mpc.DisableInput();

		if (roundCount % 2 != 0)
		{
			await PlayBottomTextAnim(ui.GetColor(waitPlayer.Mc.Team), "«–ªªªÿ∫œ", false);
		}
		else
		{
			await PlayBottomTextAnim(Color.white, "º∆À„…À∫¶", true);
			await ShowDamage();
		}
		if (roundCount % 5 == 1 && roundCount != 1)
		{
			GenerateSupplies(1);
		}

		//await ShowDamage();

		await CallSpecicalEvent();
		NewRound();
	}

	void GenerateSupplies(int count)
	{
		//AdvancedDebug.LogWarning("GenerateSupplies:" + count);
		Game.GenerateSupplies(lefts, rights, count);
	}

	private Vector3 bottomOrigin;

	async UniTask PlayBottomTextAnim(Color color, string text, bool playSound = false)
	{
		if (playSound)
		{
			source.Play();
		}
		ui.BottomText.color = Color.clear;
		ui.BottomText.text = text;
		ui.BottomText.DOColor(color, 0.5f);
		RectTransform ts = ui.BottomText.rectTransform;
		ts.DOMoveY(bottomOrigin.y + 5.0f, 0.5f);
		await UniTask.Delay(1500);
		ui.BottomText.DOColor(Color.clear, 0.5f);
		await UniTask.Delay(1000);
		ts.position = bottomOrigin;
	}

	IEnumerator GainNumber(TMP_Text txt, int total)
	{
		float tick = 0.0f;
		float res = 0.0f;
		while (tick <= 1.0f)
		{
			tick += Time.deltaTime * 8.0f;
			res = total * tick;
			txt.text = ((int)res).ToString();
			yield return new WaitForSeconds(Time.deltaTime * 8.0f);
		}
	}

	async UniTask ShowDamage()
	{
		int blueDmg = Game.ComputeDamage(gmb.MatSystem, EPlayerTeam.Blue, damagePointThisRound);
		int redDmg = Game.ComputeDamage(gmb.MatSystem, EPlayerTeam.Red, Game.ToMirrorLocation(gmb.MatSystem, damagePointThisRound));
		AdvancedDebug.LogWarning($"{blueDmg},{redDmg}");

		ui.BlueDamage.color = ui.GetColor(EPlayerTeam.Blue);
		ui.RedDamage.color = ui.GetColor(EPlayerTeam.Red);

		StartCoroutine(GainNumber(ui.BlueDamage, blueDmg));
		StartCoroutine(GainNumber(ui.RedDamage, redDmg));
		/*ui.BlueDamage.text = blueDmg.ToString();
		ui.RedDamage.text = redDmg.ToString();*/

		await UniTask.Delay(3000);
		gmb.P1Character.ApplyDamage(redDmg);
		gmb.P2Character.ApplyDamage(blueDmg);
		ui.BlueDamage.color = Color.clear;
		ui.RedDamage.color = Color.clear;
	}

	int roundCount = 0;
	bool hasWall = true;

	async UniTask CallSpecicalEvent()
	{
		if (roundCount == 6)
		{
			await PlayBottomTextAnim(Color.white, "∏Ù¿Î«Ω≤≥˝", true);
			await RemoveWall();
		}
		await UniTask.Delay(1000);
	}

	async UniTask RemoveWall()
	{
		gmb.MatSystem.BlockingWall.GetComponent<Animator>().SetTrigger("Disappear");

		hasWall = false;
		await UniTask.Delay(1000);
		gmb.MatSystem.BlockingWall.SetActive(false);
	}

	async void NewRound()
	{
		bool actDead = actPlayer.Mc.Hp <= 0;
		bool waitDead = waitPlayer.Mc.Hp <= 0;

		if (!(actDead || waitDead))
		{
			PlayerComponentGroup temp = actPlayer;
			actPlayer = waitPlayer;
			waitPlayer = temp;
			if (roundCount % 2 == 0)
			{
				ResetPoints();
			}
			OnRoundBegin();
		}
		else
		{
			await UniTask.Delay(100);
			if (actDead)
			{
				await PlayBottomTextAnim(ui.GetColor(actPlayer.Mc.Team), "Lusee’Ω∞‹£°", true);
				gmb.Victoria = waitPlayer.Mc.Team;
			}
			else
			{
				await PlayBottomTextAnim(ui.GetColor(waitPlayer.Mc.Team), "Daven’Ω∞‹£°", true);
				gmb.Victoria = actPlayer.Mc.Team;
			}

			gmb.Victory();
		}
		
	}

	void ResetPoints()
	{
		MatrixSystem matSystem = gmb.MatSystem;
		if (damagePointThisRound != new IntVector2D(-1, -1))
		{
			matSystem[damagePointThisRound].TurnNormal();
			matSystem[Game.ToMirrorLocation(matSystem, damagePointThisRound)].TurnNormal();
			damagePointThisRound = damagePointNextRound;
			damagePointNextRound = Game.GeneratePoint(matSystem, EPlayerTeam.Blue);
		}
		else
		{
			damagePointThisRound = Game.GeneratePoint(matSystem, EPlayerTeam.Blue);
			damagePointNextRound = Game.GeneratePoint(matSystem, EPlayerTeam.Blue);
		}

		matSystem[damagePointThisRound].TurnBright();
		matSystem[Game.ToMirrorLocation(matSystem, damagePointThisRound)].TurnBright();

		//AdvancedDebug.Log(damagePointThisRound);
		//AdvancedDebug.LogWarning(damagePointNextRound);
	}

	public bool DoesBlockExist()
	{
		return hasWall;
	}

	public async void DetentionToVictory(EPlayerTeam victoria)
	{
		AdvancedDebug.Log("DetentionToVictory");
		string name = victoria == EPlayerTeam.Red ? "Daven" : "Lusee";
		/*if (GameUi.Instance == null)
		{
			AdvancedDebug.LogError("ui == null!");
		}
		GameUi.Instance.enabled = true;
		await UniTask.Delay(500);
		await PlayBottomTextAnim(GameUi.Instance.GetColor(victoria), $"{name}¿ß±–£°", true);*/
		gmb = GameModeBase.Get<MatGameModeBase>();
		gmb.Victoria = victoria;
		await UniTask.Delay(100);
		gmb.Victory();
	}
}
