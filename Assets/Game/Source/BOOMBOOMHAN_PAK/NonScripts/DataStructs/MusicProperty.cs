using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicMethods = MusicProperty.MusicMethods;

[Serializable]
public class MusicProperty
{
	[SerializeField]
	private float bpm;

	[SerializeField]
	private sbyte molecule;

	[SerializeField]
	private sbyte deno;

	[SerializeField]
	private float playDelay;

	private float durationPerBeat;

	public float Bpm
	{
		get => bpm;
		private set
		{
			bpm = value;
			durationPerBeat = 60.0f / bpm;
		}
	}

	public float DurationPerBeat
	{
		get
		{
			if (durationPerBeat == 0.0f)
			{
				durationPerBeat = 60.0f / bpm;
			}

			return durationPerBeat;
		}
	}

	public sbyte BeatCountEachSection
	{
		get => molecule;
		private set => molecule = value;
	}

	public sbyte NoteEachBeat
	{
		get => deno;
		private set => deno = value;
	}

	public float PlayDelayInSeconds
	{
		get => playDelay / 1000.0f;
	}

	public MusicProperty(float bpm, sbyte molecule, sbyte deno, float playDelay)
	{
		this.bpm = bpm;
		this.molecule = molecule;
		this.deno = deno;
		this.playDelay = playDelay;
		durationPerBeat = 60.0f / bpm;
	}

	public static class MusicMethods
	{
		public static void SetBpm(MusicProperty music, float bpm)
		{
			Debug.Assert(bpm > 0.0f);
			music.Bpm = bpm;
		}

		public static float GetBeat(MusicProperty music, float time)
		{
			float playTime = time - music.PlayDelayInSeconds;
			return (playTime / music.DurationPerBeat);
		}

		public static int GetSection(MusicProperty music, float time) => (int)((GetBeat(music, time)) / music.molecule);

		public static float ToBeatInSection(MusicProperty music, float beat) => beat % music.molecule;

		public static float ToBeat(MusicProperty music, int section, float beatInSection) => section * music.molecule + beatInSection;

		public static float ToTime(MusicProperty music, float beat) => beat * music.DurationPerBeat + music.PlayDelayInSeconds;

		public static float ToTime(MusicProperty music, int section, float beatInSection) => ToTime(music, ToBeat(music, section, beatInSection));

		public static float ToDuration(MusicProperty music, float beatLength) => music.DurationPerBeat * beatLength;
	}
}

public static class MusicExtends
{
	/// <summary>
	/// 设置一个Music对象的BPM。(慎用！)
	/// </summary>
	/// <param name="music">Music对象</param>
	/// <param name="bpm">BPM值</param>
	public static void SetBpm(this MusicProperty music, float bpm) => MusicMethods.SetBpm(music, bpm);
	
	/// <summary>
	/// 获取一个Music对象的拍数。
	/// </summary>
	/// <warning>
	/// 1.不用减去PlayDelayInSeconds，因为该延迟也算在内！
	/// 2.拍数从0开始计算！
	/// </warning>
	/// <param name="music">Music对象</param>
	/// <param name="source">音频源对象</param>
	/// <returns>根据音频源的播放时长、Music对象的若干属性计算出的拍数(浮点数)</returns>
	public static float Beat(this MusicProperty music, AudioSource source) => MusicMethods.GetBeat(music, source.time);
	
	/// <summary>
	/// 获取一个Music对象的小节数。
	/// </summary>
	/// <warning>
	/// 1.不用减去PlayDelayInSeconds，因为该延迟也算在内！
	/// 2.小节数从0开始计算！
	/// </warning>
	/// <param name="music">Music对象</param>
	/// <param name="source">音频源对象</param>
	/// <returns>根据音频源的播放时长、Music对象的若干属性计算出的小节数(整数)</returns>
	public static int Section(this MusicProperty music, AudioSource source) => MusicMethods.GetSection(music, source.time);
	
	/// <summary>
	/// 将总拍数转化为在小节中的拍数。例如，在一个4/8的音乐中，9.5拍会转化为第1.5拍。
	/// </summary>
	/// <warning>
	/// 小节数和拍数从0开始计算！
	/// </warning>
	/// <param name="music">Music对象</param>
	/// <param name="beat">给定拍数</param>
	/// <returns>总拍数在当前小节中的拍数</returns>
	public static float ToBeatInSection(this MusicProperty music, float beat) => MusicMethods.ToBeatInSection(music, beat);
	
	/// <summary>
	/// 将小节数和小节中的拍数转化为总拍数。例如，在一个4/8的音乐中，第1小节第3.5拍会被转化为第11.5拍。
	/// </summary>
	/// <warning>
	/// 小节数和拍数从0开始计算！
	/// </warning>
	/// <param name="music">Music对象</param>
	/// <param name="section">给定小节数</param>
	/// <param name="beatInSection">给定小节中的拍数</param>
	/// <returns>当前小节中的拍数在音乐中的总拍数</returns>
	public static float ToBeat(this MusicProperty music, int section, float beatInSection) => MusicMethods.ToBeat(music, section, beatInSection);
	
	/// <summary>
	/// 将拍数转化为实际播放时间。
	/// </summary>
	/// <warning>
	/// 1.不用减去PlayDelayInSeconds，因为该延迟也算在内！
	/// 2.拍数从0开始计算！
	/// </warning>
	/// <param name="music">Music对象</param>
	/// <param name="beat">给定拍数</param>
	/// <returns>通过Music对象的BPM属性，计算出给定拍数对应的实际播放时间</returns>
	public static float ToTime(this MusicProperty music, float beat) => MusicMethods.ToTime(music, beat);
	
	/// <summary>
	/// 将小节数和小节中的拍数转化为实际播放时间。
	/// </summary>
	/// <warning>
	/// 1.不用减去PlayDelayInSeconds，因为该延迟也算在内！
	/// 2.小节数和拍数从0开始计算！
	/// </warning>
	/// <param name="music">Music对象</param>
	/// <param name="section">给定小节数</param>
	/// <param name="beatInSection">给定小节中的拍数</param>
	/// <returns>通过Music对象的BPM属性，计算出给定小节数和拍数对应的实际播放时间</returns>
	public static float ToTime(this MusicProperty music, int section, float beatInSection) => MusicMethods.ToTime(music, section, beatInSection);

	/// <summary>
	/// 将拍长转化为持续时间。
	/// </summary>
	/// <param name="music">Music对象</param>
	/// <param name="beatCount">拍长，可以为浮点数</param>
	/// <returns>根据Music对象的BPM属性计算出的拍长对应持续时间(按秒计算)</returns>
	public static float ToDuration(this MusicProperty music, float beatLength) => MusicMethods.ToDuration(music, beatLength);
	
	/// <summary>
	/// 从编辑器计算事件的起始时间
	/// </summary>
	/// <warning>
	/// 1.不用减去PlayDelayInSeconds，因为该延迟也算在内！
	/// 2.此处的小节数和拍数将从1开始计算！！！！！
	/// </warning>
	/// <param name="music">Music对象</param>
	/// <param name="beginSection">开启小节</param>
	/// <param name="beginBeat">开启拍</param>
	/// <returns></returns>
	public static float CalculateBeginTimeFromEditor(this MusicProperty music, int beginSection, float beginBeat)
	{
		beginSection--;
		beginBeat--;
		while (beginBeat < 0.0f)
		{
			beginBeat += music.BeatCountEachSection;
			beginSection--;
		}
		//AdvancedDebug.LogWarning($"{beginSection}	{beginBeat}	{music.ToTime(beginSection, beginBeat)}");
		return music.ToTime(beginSection, beginBeat);
	}

	/// <summary>
	/// 从编辑器计算事件的起始时间
	/// </summary>
	/// <warning>
	/// 1.不用减去PlayDelayInSeconds，因为该延迟也算在内！
	/// 2.此处的小节数和拍数将从1开始计算！！！！！
	/// </warning>
	/// <param name="music">Music对象</param>
	/// <param name="approachSection">到达小节</param>
	/// <param name="approachBeat">到达拍</param>
	/// <param name="beatLength">拍长</param>
	/// <returns></returns>
	public static float CalculateBeginTimeFromEditor(this MusicProperty music, int approachSection, float approachBeat, float beatLength)
	{
		approachSection--;
		approachBeat--;
		float beginBeat = approachBeat - beatLength;
		int beginSection = approachSection;
		while (beginBeat < 0.0f)
		{
			beginBeat += music.BeatCountEachSection;
			beginSection--;
		}
		//AdvancedDebug.LogWarning($"{beginSection}	{beginBeat}	{music.ToTime(beginSection, beginBeat)}");
		return music.ToTime(beginSection, beginBeat);
	}
}