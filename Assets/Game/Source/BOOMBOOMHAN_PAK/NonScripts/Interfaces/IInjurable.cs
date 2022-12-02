///
///
///
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ITeam))]
public abstract class IInjurable : MonoBehaviour
{
	[SerializeField]
	private AudioSource injurySource;
	
	private void Awake()
	{
		if (!injuredShader)
		{
			injuredShader = Resources.Load("InjuredShader", typeof(Shader)) as Shader;
		}
	}

	public virtual void ReceiveDamage(float dmg)
	{
		Debug.Assert(spriteRenderer != null, "没有找到SpriteRenderer。");
		StartCoroutine(DoBlink());
		injurySource.Play();
	}

	IEnumerator DoBlink()
	{
		Material material = spriteRenderer.material;
		spriteRenderer.material = new Material(injuredShader);
		//spriteRenderer.enabled = false;
		yield return new WaitForSeconds(0.1f);
		spriteRenderer.material = material;
		//spriteRenderer.enabled = true;
	}

	private static Shader injuredShader;

	private ITeam team;

	[SerializeField]
	private SpriteRenderer spriteRenderer;

	public ITeam Team
	{
		get
		{
			if (!team)
			{
				team = GetComponent<ITeam>();
			}

			return team;
		}
	}
}
