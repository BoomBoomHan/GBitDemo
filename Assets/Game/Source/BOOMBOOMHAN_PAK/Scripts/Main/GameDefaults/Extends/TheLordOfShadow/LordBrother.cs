using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class LordBrother : Brother
{
	[SerializeField, DisplayName("上影子")]
	private Transform upShadow;
	
	[SerializeField, DisplayName("下影子")]
	private Transform downShadow;

	private int newIndex;

	private bool areShadowsShown;

	private Material upMat;
	private Material downMat;
	private int dpId;

	public bool ShowShadows
	{
		get => areShadowsShown;
	}

	protected override void Awake()
	{
		base.Awake();

		areShadowsShown = false;

		WhenRun.AddListener((nIndex =>
		{
			//AdvancedDebug.Log("WhenRun");
			newIndex = nIndex;
		}));

		upMat = upShadow.GetComponent<SpriteRenderer>().material;
		downMat = downShadow.GetComponent<SpriteRenderer>().material;
		dpId = Shader.PropertyToID("_DissolveProcess");
	}

	protected override void Start()
	{
		base.Start();
	}

	public async void DOExpandShadows(float duration)
	{
		upShadow.gameObject.SetActive(true);
		downShadow.gameObject.SetActive(true);
		upMat.SetFloat(dpId, 0.0f);
		downMat.SetFloat(dpId, 0.0f);
		float distance = TrackSystem.Instance.Distance;
		float ratio = 1.0f / transform.GetChild(0).transform.localScale.x;
		upShadow.DOLocalMoveY(distance * ratio, duration);
		downShadow.DOLocalMoveY(-distance * ratio, duration);
		
		await UniTask.Delay(TimeSpan.FromSeconds(duration));
		areShadowsShown = true;
	}

	public async void DOHideShadows(float duration)
	{
		StartCoroutine(DissolveShadow(upMat, dpId, duration));
		StartCoroutine(DissolveShadow(downMat, dpId, duration));
		
		areShadowsShown = false;
		await UniTask.Delay(TimeSpan.FromSeconds(duration));
		upShadow.transform.localPosition = Vector3.zero;
		downShadow.transform.localPosition = Vector3.zero;
		upShadow.gameObject.SetActive(false);
		downShadow.gameObject.SetActive(false);
		
	}

	private float GetNextStillY()
	{
		AdvancedDebug.LogWarning(newIndex);
		return TrackSystem.Instance[newIndex].GeyY();
	}

	IEnumerator DissolveShadow(Material shadow, int id, float duration)
	{
		float speed = 1.0f / duration;
		for (float delta = 0.0f; delta < 1.0f; delta += speed * Time.fixedDeltaTime)
		{
			shadow.SetFloat(id, delta);
			yield return new WaitForFixedUpdate();
		}
		/*float delta = 0.0f;
		float speed = 1.0f / duration;
		while (delta < distance)
		{
			AdvancedDebug.Log("TranslateShadow");
			delta += speed * Time.fixedDeltaTime;
			shadow.transform.localPosition = new Vector3(0.0f, delta, 0.0f);
			yield return new WaitForFixedUpdate();
		}*/
	}
}
