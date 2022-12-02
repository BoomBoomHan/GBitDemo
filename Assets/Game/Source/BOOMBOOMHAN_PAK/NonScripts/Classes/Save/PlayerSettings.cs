///
///
///
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : Slot
{
	public float MasterVolume;

	public float MusicVolume;

	public float AffectVolume;
	
    public PlayerSettings(float masterVolume, float musicVolume, float affectVolume)
    {
	    MasterVolume = masterVolume;
	    MusicVolume = musicVolume;
	    AffectVolume = affectVolume;
    }

    public static string SlotName
    {
	    get => "PlayerSettings";
    }

    public static PlayerSettings SafeLoadSettings()
    {
	    if (Slot.TryLoadGameFromNative(out PlayerSettings ps, SlotName) == false)
	    {
		    ps = new PlayerSettings(100f, 100f, 100f);
		    Slot.SaveGameToNative(ps, SlotName);
	    }

	    return ps;
    }
}
