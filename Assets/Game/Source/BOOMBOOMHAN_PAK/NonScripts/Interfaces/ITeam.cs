///
///
///
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITeam : MonoBehaviour
{
	[SerializeField]
	private TeamType teamType;

	/*public TeamType WhichTeam
	{
		get
		{
			return teamType;
		}
	}*/

	public ITeam()
	{
		teamType = TeamType.Purple;
	}

	public bool IsNotAllies(ITeam receiver)
	{
		if (teamType == TeamType.None) return true;
		return teamType != receiver.teamType;
	}

	public static bool AreNotAllies(TeamType attacker, ITeam receiver)
	{
		if (attacker == TeamType.None) return true;
		return attacker != receiver.teamType;
	}
}
