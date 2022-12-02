using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	private float speed;

	public float Speed
	{
		get => speed;
		set
		{
			if (value > 0.0f)
			{
				speed = value;
			}
		}
	}

	void Awake()
	{
		
	}
	
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);
    }

    public virtual void Init(BombSpawnParameters parameters)
    {
	    //Debug.LogWarning("Init " + gameObject.name);
	    gameObject.SetActive(true);

	    speed = parameters.Speed;
	    transform.position = new Vector2(TrackSystem.Instance.SpawnX, TrackSystem.Instance[parameters.TrackIndex].GeyY());
	    
    }

    public virtual void OnRemove()
    {
	    gameObject.SetActive(false);
    }
}
