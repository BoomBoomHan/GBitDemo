using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Input;

public class BroController : PlayerController
{
	private KeyCode toUpperKey;

	private KeyCode toLowerKey;

	private Brother brother;
	private BroState broState;

	public BroController()
	{
		toUpperKey = KeyCode.W;
		toLowerKey = KeyCode.S;
	}

	protected override void Start()
	{
		base.Start();
		
		brother = defaultCharacter as Brother;
		broState = defaultPlayerState as BroState;
	}

	protected override void InputTick(float deltaTime)
	{
		if (GetKeyDown(toUpperKey) && !broState.IsBroRunning())
		{
			bool tryMove = brother.Run(-1);
		}
		
		if (GetKeyDown(toLowerKey) && !broState.IsBroRunning())
		{
			bool tryMove = brother.Run(1);
		}
	}

	protected override void InputFixedTick(float fixedDeltaTime)
	{
		
	}

	protected override void InputTickAlways(float deltaTime)
	{
		if (GetKeyDown(KeyCode.Escape))
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}
	}
}
