using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{

    // Start is called before the first frame update
    protected virtual void Start()
    {
	    gamePaused = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    private static string musicGroupName
    {
	    get => "MusicGroup";
    }
    
    private static HashSet<AudioSource> musicSet = new HashSet<AudioSource>();
    
    private static bool gamePaused = false;
    
    public static bool IsGamePaused
    {
	    get => gamePaused;
    }
    
    public static void PauseGame(bool pauseMusic = true, UnityEvent onPause = null)
    {
	    if (gamePaused) return;
	    
	    gamePaused = true;
	    Time.timeScale = 0f;
	    if (pauseMusic)
	    {
		    var audioSources = GameObject.FindObjectsOfType<AudioSource>();
		    foreach (var audio in audioSources)
		    {
			    //Debug.LogWarning(audio.outputAudioMixerGroup.name);
			    if (audio.outputAudioMixerGroup.name == musicGroupName && audio.isPlaying)
			    {
				    audio.Pause();
				    musicSet.Add(audio);
			    }
		    }
		    //Debug.LogWarning(audioArray.Length);
	    }
    	    
	    onPause?.Invoke();
    }
    
    public static void ResumeGame(UnityEvent onResume = null)
    {
	    if (!gamePaused) return;
	    
	    gamePaused = false;
	    Time.timeScale = 1f;
	    onResume?.Invoke();
    	    
	    foreach (var music in musicSet)
	    {
		    music.Play();
	    }
    }
}
