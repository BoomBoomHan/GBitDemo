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

    public static int ToNumber(Floor floor)
    {
        return floor.Number;
    }
}
