using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyFloor : SpecialFloor
{
    [SerializeField]
    SpriteRenderer supplyRenderer;

    MatGameModeBase gmb;

    private SupplyObject sObject;

    public SupplyObject SObject
    {
        get => sObject;
        set
        {
            sObject = value;
        }
    }

    private void Start()
    {
        gmb = GameModeBase.Get<MatGameModeBase>();
        gmb.P1Character.MoveEnd.AddListener(JudgeCharacterEnter);
		gmb.P2Character.MoveEnd.AddListener(JudgeCharacterEnter);
	}

    void JudgeCharacterEnter(IntVector2D location)
    {
        if (location == Coord)
        {
            var mc = gmb.P1Character.Location == location ? gmb.P1Character : gmb.P2Character;
            OnCharacterEnter(mc);
        }
    }

    public bool CanCharacterEnter(bool characterHasSupplies)
    {
        return !(characterHasSupplies && SObject != null);
    }

    public override void OnCharacterEnter(MatCharacter character)
    {
        sObject = null;
    }

    float tick = 0.0f;

    protected override void Update()
    {
        base.Update();
        tick += Time.deltaTime * 0.5f;
        Vector3 target = supplyRenderer.transform.localPosition;
        target.y = Mathf.Sin(tick) * 0.15f + 0.45f;
        supplyRenderer.transform.localPosition = target;

        supplyRenderer.sprite = sObject != null ? sObject.SSprite : null;
    }
}
