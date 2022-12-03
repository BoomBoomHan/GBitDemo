using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public EPlayerTeam PlayerTeam;

    public int InitialHp;

    public PlayerData()
    {
        PlayerTeam = EPlayerTeam.Blue;
        InitialHp = 100;
    }

    public PlayerData(EPlayerTeam playerTeam, int initialHp)
    {
        PlayerTeam = playerTeam;
        InitialHp = initialHp;
    }
}
