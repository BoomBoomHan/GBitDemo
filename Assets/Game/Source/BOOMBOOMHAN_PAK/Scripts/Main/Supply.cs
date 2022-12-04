using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;

    public virtual void OnCharacterPick(MatrixSystem matrixSystem, MatCharacter character, MatPlayerState state)
    {
        //character
    }

    private void Update()
    {
		Vector3 target = transform.position;
		target.z = MatrixSystem.ZLocation;
		transform.position = target;
	}
}
