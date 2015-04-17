using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundPlayer {
	
	GameObject soundPlayerObj;
	AudioSource audioSource;
	Dictionary<string, AudioClipInfo> audioClips = new Dictionary<string, AudioClipInfo>();

	private float volume=0.7f;
	public float VOLUME{
		get{
            if (!setted && PlayerPrefs.HasKey("volumeKey"))
            {
                setted = true;
                Singleton<SoundPlayer>.instance.VOLUME = PlayerPrefs.GetFloat("volumeKey");
            }
            return volume;
		}
		set{
            setted = true;
			volume = value;
		}
	}

    bool setted = false;

	// AudioClip information
	class AudioClipInfo {
		public string resourceName;
		public string name;
		public AudioClip clip;
		
		public AudioClipInfo( string resourceName, string name ) {
			this.resourceName = resourceName;
			this.name = name;
		}
	}
	
	public SoundPlayer() {
		audioClips.Add( "enter", new AudioClipInfo( "decision4", "enter" ) );
		audioClips.Add( "draw", new AudioClipInfo( "draw", "draw" ) );
		audioClips.Add( "freeze", new AudioClipInfo( "magic-ice2", "freeze" ) );
		audioClips.Add( "attack", new AudioClipInfo( "punch-high1", "attack" ) );
		audioClips.Add( "summon", new AudioClipInfo( "specialsummon", "summon" ) );
		audioClips.Add( "signiSummon", new AudioClipInfo( "summon", "signiSummon" ) );
		audioClips.Add( "effect", new AudioClipInfo( "activate", "effect" ) );
		audioClips.Add( "decision", new AudioClipInfo( "decision8", "decision" ) );
		audioClips.Add( "banish", new AudioClipInfo( "glass-break4", "banish" ) );
		audioClips.Add( "crash", new AudioClipInfo( "magic-ice4", "crash" ) );

	}
	
	public bool playSE( string seName ) {
		if ( audioClips.ContainsKey( seName ) == false )
			return false; // not register

		AudioClipInfo info = audioClips[ seName ];
		
		// Load
		if ( info.clip == null )
			info.clip = (AudioClip)Resources.Load( "sound/"+info.resourceName );
		
		if ( soundPlayerObj == null ) {
			soundPlayerObj = new GameObject( "SoundPlayer" );
			audioSource = soundPlayerObj.AddComponent<AudioSource>();
		}
		
		// Play SE
		audioSource.PlayOneShot( info.clip, VOLUME);
		
		return true;
	}
}