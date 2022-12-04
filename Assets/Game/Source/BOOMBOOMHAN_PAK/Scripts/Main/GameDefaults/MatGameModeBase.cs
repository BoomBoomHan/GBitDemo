using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatGameModeBase : GameModeBase
{
    private MatCharacter mc0;
	private MatCharacter mc1;

	public MatCharacter P1Character
	{
		get => mc0;
	}
	public MatCharacter P2Character
	{
		get => mc1;
	}

	private MatPlayerController mpc0;
	private MatPlayerController mpc1;

	public MatPlayerController P1Controller
	{
		get => mpc0;
	}
	public MatPlayerController P2Controller
	{
		get => mpc1;
	}

	private MatPlayerState mps0;
	private MatPlayerState mps1;

	public MatPlayerState P1State
	{
		get => mps0;
	}
	public MatPlayerState P2State
	{
		get => mps1;
	}

	[SerializeField, GameModeProperty(Category = "主要", DisplayName = "矩阵系统")]
	private MatrixSystem matrixSystem;

	[SerializeField, GameModeProperty(Category = "UI", DisplayName = "游戏UI资源")]
	private GameUi gameUiResource;

	public GameUi Ui
	{
		get;
		private set;
	}

	public MatrixSystem MatSystem
	{
		get => matrixSystem;
	}

	protected async override void Awake()
    {
		if (Instance)
		{
			Debug.LogError("Fatal!");
			Debug.LogError("GameModeBase存在多个实例！");
		}

		Instance = this;

		Ui = Instantiate(gameUiResource).GetComponent<GameUi>();

		mc0 = Instantiate(defaultCharacter, PlayerStartPosition, Quaternion.identity).GetComponent<MatCharacter>();
		mc1 = Instantiate(defaultCharacter, PlayerStartPosition, Quaternion.identity).GetComponent<MatCharacter>();

		mpc0 = Instantiate(defaultPlayerController).GetComponent<MatPlayerController>();
		mpc1 = Instantiate(defaultPlayerController).GetComponent<MatPlayerController>();
		InstantiateDefaults(ref defaultGameState);
		mps0 = Instantiate(defaultPlayerState).GetComponent<MatPlayerState>();
		mps1 = Instantiate(defaultPlayerState).GetComponent<MatPlayerState>();

		mpc0.BindCharacter(mc0);
		mpc1.BindCharacter(mc1);

		mpc0.BindPlayerState(mps0);
		mpc1.BindPlayerState(mps1);
		mps0.BindCharacter(mc0);
		mps1.BindCharacter(mc1);

		/*mps0.Team = EPlayerTeam.Blue;
		mps1.Team = EPlayerTeam.Red;*/

		await UniTask.Delay(40);
		Game.GetPlayerStartPositions(matrixSystem.Size, out IntVector2D p1Start, out IntVector2D p2Start);
		Vector2 zeroPoint = matrixSystem.ZeroPoint;
		mc0.transform.position = zeroPoint + new Vector2(matrixSystem.Distance * p1Start.Y, -matrixSystem.Distance * p1Start.X);
		mc0.Location = p1Start;
		mc0.Team = EPlayerTeam.Blue;
		//mc0.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
		matrixSystem.SetCharacterEnter(p1Start);

		mc1.transform.position = zeroPoint + new Vector2(matrixSystem.Distance * p2Start.Y, -matrixSystem.Distance * p2Start.X);
		mc1.Location = p2Start;
		mc1.Team = EPlayerTeam.Red;
		mpc1.HorizontalAxisName = "P2Horizontal";
		mpc1.VerticalAxisName = "P2Vertical";
		//mc1.GetComponentInChildren<SpriteRenderer>().color = Color.red;
		matrixSystem.SetCharacterEnter(p2Start);

		if (!GameInstance.Instance)
		{
			InstantiateDefaults(ref gameInstance);
		}
		

		UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
		Application.targetFrameRate = -1;
	}

	void InitBlue(IntVector2D startLocation)
	{
		mc0.Team = EPlayerTeam.Blue;

		mc0.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
	}

	void InitRed(IntVector2D startLocation)
	{
		mc1.Team = EPlayerTeam.Red;
		mpc1.HorizontalAxisName = "P2Horizontal";
		mpc1.VerticalAxisName = "P2Vertical";

		mc1.GetComponentInChildren<SpriteRenderer>().color = Color.red;
	}

	protected override void Update()
	{
		base.Update();

		if (mc0 == null || mc1 == null)
		{
			return;
		}

		Ui.BlueHpBar.value = mc0.HpPercentage;
		Ui.RedHpBar.value = mc1.HpPercentage;
		Ui.BlueHpText.text = mc0.Hp.ToString();
		Ui.RedHpText.text = mc1.Hp.ToString();
	}
}
