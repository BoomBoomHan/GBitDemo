///
///
///
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettingBinder : MonoBehaviour
{
	private Slider boundSlider;
	
	[SerializeField]
	private TMP_Text txt;

	private void Awake()
	{
		boundSlider = GetComponent<Slider>();
	}

	public void Refresh(float value)
    {
	    txt.SetText(value.ToString("F0"));
        //txt.SetText((100 * ((value - boundSlider.minValue) / (boundSlider.maxValue - boundSlider.minValue))).ToString("F0"));
    }
}
