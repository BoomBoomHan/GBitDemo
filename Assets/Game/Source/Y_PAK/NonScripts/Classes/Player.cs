using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete]
public class Player
{
    
    public EPlayerTeam PlayerTeam;

    public float InitiaHP;

    public int NumberSteps;


    public Player()
    {
        PlayerTeam = EPlayerTeam.Blue;
        InitiaHP = 100;
        NumberSteps = 5;
    }

    public Player(EPlayerTeam playerTeam ,float initiaHP,int Steps)
    {
        PlayerTeam = playerTeam;
        InitiaHP = initiaHP;
        NumberSteps = Steps;
    }
}
