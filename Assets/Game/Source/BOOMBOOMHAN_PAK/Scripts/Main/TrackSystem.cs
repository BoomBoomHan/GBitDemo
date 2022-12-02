using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSystem : MonoBehaviour
{
	[SerializeField]
	private TrackPoint[] trackPoints;

	[SerializeField]
	private GameObject spawnPoint;

	[SerializeField, DisplayName("完美音效")]
	private AudioClip perfectClip;

	[SerializeField, DisplayName("受伤音效")]
	private AudioClip injureClip;

	private AudioSource source;

	public static TrackSystem Instance
	{
		get;
		private set;
	}

	public float SpawnX
	{
		get;
		private set;
	}

	public float BoomX
	{
		get;
		private set;
	}

	public TrackPoint[] Tracks
	{
		get => trackPoints;
	}

	public float Distance
	{
		get;
		private set;
	}

	private void Awake()
	{
		Debug.Assert(Instance == null);
		Instance = this;
		SpawnX = spawnPoint.transform.position.x;
		BoomX = trackPoints[0].transform.position.x;
		Distance = trackPoints[0].transform.position.y - trackPoints[1].transform.position.y;

		for (int i = 0; i < trackPoints.Length; i++)
		{
			trackPoints[i].TrackIndex = i;
		}

		source = gameObject.AddComponent<AudioSource>();
		source.playOnAwake = false;
		source.loop = false;
		source.volume = 0.75f;

		var evt = GameModeBase.Get<BroGameModeBase>().HitEvent;
		evt.AddListener(PlaySound);
		evt.AddListener(ShinePoint);
	}

	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TrackPoint this[int index]
    {
	    get => trackPoints[index];
    }

    void PlaySound(BombHitResult result)
    {
	    source.clip = result.HasInjure ? injureClip : perfectClip;
	    source.Play();
    }

    void ShinePoint(BombHitResult result)
    {
	    var records = result.Records;
	    foreach (var record in records)
	    {
		    if (record.Value)
		    {
			    trackPoints[record.Key].OnReceiveBomb();
		    }
	    }
    }
}
