using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatPlayerController : PlayerController
{
    public string HorizontalAxisName
    {
        set => horizontalAxisName = value; 
    }
    public string VerticalAxisName
    {
        set => verticalAxisName = value; 
    }
}
