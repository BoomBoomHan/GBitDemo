using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BroAnimation : MonoBehaviour
{
	private new SpriteRenderer renderer;
	private Animator animator;
	private Brother brother;

	private void Awake()
	{
		renderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		brother = GetComponentInParent<Brother>();
	}

	private void Start()
	{
		brother.WhenInjure.AddListener(OnInjured);
	}

	void OnInjured(int arg)
	{
		if (renderer.color != Color.white)
		{
			return;
		}
		renderer.color = Color.red;
		renderer.DOColor(Color.white, 0.25f);
		//animator.SetTrigger("Injured");
	}

	public void OnInjureEnd()
	{
		animator.SetTrigger("InjureEnd");
	}
}
