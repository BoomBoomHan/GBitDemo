using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFloor : Floor
{
    public override int Number { get => 0; set => throw new NotSupportedException("Number�ڴ����в�֧��set��"); }

    public override void OnCharacterEnter(MatCharacter character)
    {
        //base.OnCharacterEnter(character);
    }
}
