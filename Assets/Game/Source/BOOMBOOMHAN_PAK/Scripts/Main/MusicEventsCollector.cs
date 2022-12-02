using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicEventsCollector : MonoBehaviour
{
	[DisplayName("关卡序号(仅供CSV使用)")]
	public int LevelOrder;

	public AddBombEventCreator[] AddBombCreators;

	[HideInInspector]
	public CustomEventCreator[] CustomCreators;

	public GameOverEventCreator GameOverCreator;

	void GenerateCustomEvents(int levelOrder)
	{
		CustomCreators = CustomEventInLevels.Get(levelOrder);
	}

	public MusicEvent[] GetMusicEventsArray(MusicProperty musicProperty, int levelOrder)
	{
		GenerateCustomEvents(levelOrder);
		
		MusicEvent[] result = new MusicEvent[AddBombCreators.Length + CustomCreators.Length + 2];
		
		result[0] = new StartEvent();
		int index = 1;
		foreach (var creator in AddBombCreators)
		{
			result[index] = new AddBombEvent(musicProperty, creator);
			index++;
		}
		
		foreach (var creator in CustomCreators)
		{
			result[index] = CustomEventLibrary.Create(musicProperty, creator);
			index++;
		}

		//result[index] = new RevertEvent(musicProperty, 29, 1.0f, 79, 5.0f);
		result[index] = new GameOverEvent(musicProperty, GameOverCreator);

		//result[index + 2] = new PulseCameraEvent(musicProperty, 2, 1.0f, musicProperty.ToDuration(1.5f));
		return result;
	}
	
	[ContextMenu("创建CSV表")]
	public void CreateCsvFile()
	{
		CsvHelper.TryCreateDataDirectory($"Level{LevelOrder}");
		CsvHelper.CreatorCsv($"Level{LevelOrder}/Bombs", "ApproachSection", "ApproachBeat", "BeatLength", "TrackOrder");
	}

	[ContextMenu("从CSV表中读取数据")]
	public void LoadFromCsv()
	{
		var result = CsvHelper.LoadFromCsv($"Level{LevelOrder}/Bombs");
		int rows = result.GetLength(0);
		int cows = result.Length / rows;
		AddBombCreators = new AddBombEventCreator[rows - 1];
		for (int i = 1; i < rows; i++)
		{
			AddBombCreators[i - 1].ApproachSection = int.Parse(result[i, 0]);
			AddBombCreators[i - 1].ApproachBeat = float.Parse(result[i, 1]);
			AddBombCreators[i - 1].BeatLength = float.Parse(result[i, 2]);
			AddBombCreators[i - 1].TrackOrder = int.Parse(result[i, 3]);
		}
	}
	
	
}
