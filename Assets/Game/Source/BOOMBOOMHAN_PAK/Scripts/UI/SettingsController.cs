///
///
///
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
	[SerializeField, DisplayName("总音量滑条")]
	private Slider masterVolumeSlider;
	
	[SerializeField, DisplayName("音乐音量滑条")]
	private Slider musicVolumeSlider;
	
	[SerializeField, DisplayName("音效滑条")]
	private Slider affectVolumeSlider;

	[SerializeField, DisplayName("返回按钮")]
	private Button backButton;

	public Slider MasterVolumeSlider
	{
		get => masterVolumeSlider;
	}
	
	public Slider MusicVolumeSlider
	{
		get => musicVolumeSlider;
	}
	
	public Slider AffectVolumeSlider
	{
		get => affectVolumeSlider;
	}

	public Button BackButton
	{
		get => backButton;
	}
	
    public SettingsController()
    {
        
    }

    private void Start()
    {
	    PlayerSettings ps = PlayerSettings.SafeLoadSettings();
	    masterVolumeSlider.value = ps.MasterVolume;
	    musicVolumeSlider.value = ps.MusicVolume;
	    affectVolumeSlider.value = ps.AffectVolume;
	    
	    backButton.onClick.AddListener(SaveChoices);
	    backButton.onClick.AddListener(CloseSettingsPanel);
	    
	    masterVolumeSlider.onValueChanged.AddListener(GameAudioManager.Instance.SetMasterVolume);
	    musicVolumeSlider.onValueChanged.AddListener(GameAudioManager.Instance.SetMusicVolume);
	    affectVolumeSlider.onValueChanged.AddListener(GameAudioManager.Instance.SetAffectVolume);
    }

    void SaveChoices()
    {
	    Slot.SaveGameToNative(new PlayerSettings(masterVolumeSlider.value, musicVolumeSlider.value, affectVolumeSlider.value), PlayerSettings.SlotName);
    }

    void CloseSettingsPanel()
    {
	    GetComponent<Canvas>().enabled = false;
	    Destroy(gameObject, 0.15f);
    }
}
