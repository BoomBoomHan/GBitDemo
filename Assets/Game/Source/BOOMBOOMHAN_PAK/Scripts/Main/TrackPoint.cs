using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TrackPoint : MonoBehaviour
{
	private int trackIndex;

	public int TrackIndex
	{
		set => trackIndex = value;
	}
	
	private float yLocation;

	private new SpriteRenderer renderer;
	private AudioSource source;

	private static float soundInvokeTime = -1.0f;
	
	private void OnDrawGizmos()
	{
		Gizmos.DrawCube(transform.position, new Vector3(0.5f, 0.5f, 0.1f));
	}

	private void Awake()
	{
		renderer = GetComponentInChildren<SpriteRenderer>();
		source = GetComponent<AudioSource>();
	}

	// Start is called before the first frame update
    void Start()
    {
	    yLocation = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GeyY()
    {
	    return yLocation;
    }

    public void OnReceiveBomb()
    {
	    Color origin = renderer.color;
	    renderer.color = Color.yellow;
	    renderer.DOColor(origin, 0.15f);

	    float time = Time.time;
	    if (time == soundInvokeTime)
	    {
		    return;
	    }

	    soundInvokeTime = time;
	    source.Play();
    }
}
