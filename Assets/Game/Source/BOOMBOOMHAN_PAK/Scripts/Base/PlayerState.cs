using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
	protected Character defaultCharacter;
	
	protected virtual void Awake()
	{
		
	}

	protected virtual void OnEnable()
	{
		
	}

	protected virtual void Start()
	{
		
	}
	
	protected virtual void Update()
	{
	    
	}

	protected virtual void FixedUpdate()
	{
	    
	}

	protected virtual void LateUpdate()
	{
	    
	}

	protected virtual void OnDisable()
	{
	    
	}

	protected virtual void OnDestroy()
	{
		StopAllCoroutines();
	}
	
	public void BindCharacter(Character mc)
	{
		if (!defaultCharacter)
		{
			defaultCharacter = mc;
			/*if (!mc)
				Debug.Log("BindCharacter  " + mc.name);*/
			return;
		}
		Debug.LogError("Fatal!");
		Debug.LogError("在defaultCharacter已有绑定时尝试绑定其他角色！");
	}
}
