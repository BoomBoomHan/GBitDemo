using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status : ushort
{
	Idle = 0,
	Running = 1,
	Injuring = 1 << 15,
}

public class BroState : PlayerState
{
	private Brother brother;

	public ushort broStatus;

	protected override void Start()
	{
		base.Start();
		
		brother = defaultCharacter as Brother;
		broStatus = 0;
		brother.WhenRun.AddListener(OnBroRun);
		brother.WhenStopRunning.AddListener(OnBroStopRunning);
	}

	private void OnBroRun(int nIndex)
	{
		SetStatus(Status.Running);
		ResetStatus(Status.Idle);
	}

	private void OnBroStopRunning(float duration)
	{
		SetStatus(Status.Idle);
		ResetStatus(Status.Running);
	}

	private void SetStatus(Status status)
	{
		broStatus |= (ushort)status;
	}

	private void ResetStatus(Status status)
	{
		broStatus &= (ushort)(~(ushort)status);
	}

	private bool HasStatus(Status status)
	{
		return (broStatus & (ushort)status) != 0;
	}

	public bool IsBroRunning()
	{
		return HasStatus(Status.Running);
	}

	public bool IsBroInjuring()
	{
		return HasStatus(Status.Injuring);
	}

	/*protected override void Update()
	{
		base.Update();
	}*/
}
