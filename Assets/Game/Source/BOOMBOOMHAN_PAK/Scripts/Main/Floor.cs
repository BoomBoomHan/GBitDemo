using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private int number;

    public int Number
    {
        get => number;
        set => number = value;
    }

    [SerializeField, DisplayName("ÖÐÐÄµã")]
    Transform centerPoint;

    public bool HasCharaterUpon
    {
        get;set;
    }


    public static int ToNumber(Floor floor)
    {
        return floor.Number;
    }

    public static implicit operator Vector2(Floor floor)
    {
        return floor.centerPoint.transform.position;
    }
}
